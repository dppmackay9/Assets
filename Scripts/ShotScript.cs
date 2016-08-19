using UnityEngine;

public class ShotScript : MonoBehaviour {


    public int damage = 1;
    public bool isEnemyShot = true;


	// Use this for initialization
	void Start () 
    {
	    // limite le temps de vie
        Destroy(gameObject, 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
