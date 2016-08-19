using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    /// <summary>
    /// Récupère le script EnemyScript associé
    /// </summary>
    public EnemyScript enemy;

    /// <summary>
    /// Récupère le script PlayerScript associé
    /// </summary>
    public PlayerScript player;

    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 1;

    /// <summary>
    /// Enemy or player?
    /// </summary>
    public bool isEnemy = true;

  

    /// <summary>
    /// Décors ?
    /// </summary>
    public bool isDecors = false;


    /// <summary>
    /// Déterine si oui ou non le tir doit toucher
    /// </summary>
    public bool isTouche = true;


    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;
        if (hp <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        // Retrieve the enemy only once
        enemy = GetComponent<EnemyScript>();

        player = GetComponent<PlayerScript>();
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // est-ce que c'est un shot ?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {

            if (isDecors)
            {
                // Détruit le tir dans le décors
                Destroy(shot.gameObject);
            }
            else
            {
                // entre en collision avec quelque chose qui n'est pas du décors : joueur ou ennemi

                // Avoid friendly fire
                if (shot.isEnemyShot != isEnemy)
                {
                    if (shot.isEnemyShot == true && isEnemy == false)
                    {
                        // tir ennemi sur le joueur
                        // si en roulage : pas de touche !

                        if (player.isInvulnerable)
                        {
                            isTouche = false;
                        }
                    }

                    if (isTouche)
                    {
                        // le tir touche
                        Damage(shot.damage);

                        // Détruit le tir
                        Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script

                    }

                    isTouche = true;
                }




                if (isEnemy)
                {
                    // ennemi touché : on le réveille quoi qu'il arrive
                    enemy.awake = 1;

                    // maintenant on réveille tous les ennemis présents dans un certain rayon
                    // TODO

                }

            }


        }
    }
}
