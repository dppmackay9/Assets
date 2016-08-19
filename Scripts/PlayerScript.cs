using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int vitesseDefaut;
	public int vitesseRoulade = 5;

    // vitesse du joueur
	public Vector2 speed = new Vector2(3, 3);
    private Vector2 movement;

    public Sprite playerSprite;
    private Animator animator;

    public float shoot;

    public bool regardeGauche = false;

    public bool isInvulnerable;
    public float invulnerableCooldown;
	public float maxRouladeCooldown = 0.7f;
    public bool isImmobile;
    public bool isRoulade;


    void Awake()
    {
        // Get the animator
        DontDestroyOnLoad(transform.gameObject);
        animator = GetComponent<Animator>();
    }


	// Use this for initialization
	void Start () {

	}
	


	// Update is called once per frame
	void Update () 
    {
        isImmobile = true;

        // récupération des infos de déplacement
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        movement = new Vector2(speed.x * inputX, speed.y * inputY);
        if (movement.magnitude > 0f)
        {
            isImmobile = false;
        }


        if (inputX > 0 && regardeGauche == true || inputX < 0 && regardeGauche == false)
        {
            // changement de direction : on flip le sprite
            regardeGauche = !regardeGauche;

            Vector3 thescale = transform.localScale;
            thescale.x *= -1;
            transform.localScale = thescale;
        }


		////////////////
        // shoot ?
		////////////////

        shoot = Input.GetAxis("ShootGachette");
        if (shoot > 0.5f)
        {
            isImmobile = false;

            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null && (Input.GetAxis("HorizontalSecondAxis") != 0 || Input.GetAxis("VerticalSecondAxis") != 0))
            {
                // false because the player is not an enemy
                weapon.Attack(false, null, 3);
            }
        }



		///////////////
        // roulade ?
		///////////////

        if (!isRoulade)
        {
            bool doRoulade = Input.GetButtonDown("Roulade");
            if (doRoulade && (inputX != 0 || inputY != 0) )
            {
                // roulade effectuée 
                isInvulnerable = true;
                isRoulade = true;
                this.speed.x = vitesseRoulade;
                this.speed.y = vitesseRoulade;

                invulnerableCooldown = maxRouladeCooldown;
            }
        }
        
		

    	if (isRoulade){
			invulnerableCooldown -= Time.deltaTime;
		}
		else {
			invulnerableCooldown = 0;
		}
        

		if (invulnerableCooldown <= 0f)
		{
			// cooldown de la roulade dépassé : on sort de l'état "roulade"
			isInvulnerable = false;
			isRoulade = false;
			this.speed.x = vitesseDefaut;
			this.speed.y = vitesseDefaut;
		}


		// Set or unset the roulade animation
		animator.SetBool("isRoulade", isRoulade); 

	}




    void FixedUpdate()
    {
        // déplacement
        GetComponent<Rigidbody2D>().velocity = movement;

        // Set or unset the immobile animation
        animator.SetBool("isImmobile", isImmobile);
    }
}
