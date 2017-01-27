using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {
    public float speed = 10;
	private GameObject explosion;
	// Use this for initialization
	void Start () {
		explosion = GameObject.FindWithTag ("GameController").GetComponent<GameScript> ().getShotExplosion ();
		StartShot ();
	}

	void StartShot(){
		switch (gameObject.tag) {
			case "Shot":
				GetComponent<Rigidbody> ().velocity = transform.right * speed;
				break;
			case "EnemyShot":
				GetComponent<Rigidbody> ().velocity = transform.right * -speed;
				break;
		}
	}

	void OnTriggerEnter(Collider other){
		switch (other.tag) {
			case "EnemyShot":
				if (gameObject.tag != "EnemyShot")
					Explode ();
				Destroy (other.gameObject);
				break;
		}	
	}

	public void Explode(){
		if (explosion != null)
			Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

}
