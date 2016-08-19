using UnityEngine;
using System.Collections;

public class ViseurScript : MonoBehaviour {

    public float inputXSecondStick;
    public float inputYSecondStick;
    public Vector2 speed = new Vector2(5, 5);
    private Vector2 direction;

    public float positionJoueurX;
    public float positionJoueurY;
    public float positionJoueurZ;

	/// <summary>

	/// </summary>
	public SpriteRenderer sprite;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        // récupération des infos d'orientation du viseur
        inputXSecondStick = Input.GetAxis("HorizontalSecondAxis") * 10;
        inputYSecondStick = Input.GetAxis("VerticalSecondAxis") * 10;
        direction = new Vector2( (inputXSecondStick * speed.x), -((inputYSecondStick * speed.y)) );

        // récupération de la positon du joueur
        positionJoueurX = transform.parent.position.x;
        positionJoueurY = transform.parent.position.y;
        positionJoueurZ = transform.parent.position.z;
        
	}



    void FixedUpdate()
    {
		if (inputXSecondStick == 0 && inputYSecondStick == 0) 
		{
			// stick en position neutre : on cache le viseur
			sprite.enabled = false;
		} 
		else 
		{
			// on affiche le viseur
			sprite.enabled = true;
		}


		// centre le viseur
		Vector3 newPos = new Vector3(positionJoueurX, positionJoueurY, positionJoueurZ);
		transform.position = newPos;
		
		// déplace vers la position du vecteur
		GetComponent<Rigidbody2D>().velocity = direction;
       
    }

}
