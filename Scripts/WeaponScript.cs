using UnityEngine;
using System.Collections;


/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{

    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 0.25f;
    public float imprecision = 1.0f;

  


    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(bool isEnemy, EnemyScript tireur, int nbTirs=1)
    {
        if (CanAttack)
        {
            Vector2 speed = new Vector2(1, 1);
            Vector2 direction;
            //Vector3 Transform;


            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot in the good direction
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();

            if (!isEnemy)
            {
                // tir du joueur
                if (move != null)
                {
                    // récupération des infos d'orientation du viseur
                    float inputXSecondStick = Input.GetAxis("HorizontalSecondAxis");
                    float inputYSecondStick = Input.GetAxis("VerticalSecondAxis");
                    direction = new Vector2((inputXSecondStick * speed.x), -((inputYSecondStick * speed.y)));

                    if (direction.magnitude > 0f)
                    {
                        // angle d'orientation du tir
                        shot.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-(Input.GetAxis("VerticalSecondAxis")), Input.GetAxis("HorizontalSecondAxis")) * 180 / Mathf.PI);
                   
                        // normalisation de la vitesse du tir
                        direction = direction.normalized;

                        // direction du tir
                        move.direction = direction;
                    }

                    // vibration ? possible ?

                    // bruitage
                    SoundEffects.Instance.MakePlayerShotSound();


                }             
         
            }
            else
            {
                // tir ennemi
                //-- direction du tir

                // cherche le joueur
                var player = GameObject.Find("ObjetJoueur");

                // direction du tir : entre la position de l'ennemi et position du joueur
                float directionTirX = player.transform.position.x - tireur.transform.position.x;
                float directionTirY = player.transform.position.y - tireur.transform.position.y;

                // imprécision pour dévier aléatoirement le tir
                float randomNumberX = Random.Range(-imprecision, imprecision);
                float randomNumberY = Random.Range(-imprecision, imprecision);

                // direction du tir
                direction = new Vector2(directionTirX + randomNumberX, directionTirY + randomNumberY);

                // angle d'orientation du tir
                shot.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-(direction.x), direction.y) * 180 / Mathf.PI);

                // normalisation de la vitesse du tir
                direction = direction.normalized;

                // direction du tir
                move.direction = direction;

                // bruitage
                SoundEffects.Instance.MakeEnemyShotSound();
            }

        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }

    

}
