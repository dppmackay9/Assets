using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneEnter : MonoBehaviour {

    BoxCollider2D boxCollider2D;
    public string nomScene;

    // Use this for initialization
    void Start () {
        //boxCollider2D  = GetComponent<BoxCollider2D>();
 
    }
	
	// Update is called once per frame
	void Update () {
	
	}



    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        PlayerScript player = otherCollider.gameObject.GetComponent<PlayerScript>();
        if (player)
        {  
            SceneManager.LoadScene(nomScene);
        }
        
    }
    void OnTriggerExit2D(Collider2D player)
    {

    }
    
}