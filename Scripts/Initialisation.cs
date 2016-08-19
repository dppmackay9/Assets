using UnityEngine;
using System.Collections;

public class Initialisation : MonoBehaviour {

    public Transform ObjetJoueur;
    public Vector3 positionInitiale;

    void Awake()
    {
        
    }


    // Use this for initialization
    void Start () {

        positionInitiale = new Vector3(-4, 4, 0);

        // créé le joueur si il n'existe pas déjà
        GameObject myJoueur = GameObject.Find("ObjetJoueur");
        if (!myJoueur)
        {
            // instancie le joueur
            Transform g = (Transform)Instantiate(ObjetJoueur, positionInitiale, Quaternion.identity);
            // renommé pour ne pas avoir (copie) dans le nom
            g.name = "ObjetJoueur"; 

        }
        else
        {
            // on bouge juste le joueur à la position souhaitée
            myJoueur.transform.position = positionInitiale;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
