using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveyPatternScript : MonoBehaviour {
	private Boundary spawnBoundary;
	private static int SPEED = 5;
	bool topFirst;

	// Use this for initialization
	void Start () {
		spawnBoundary = GameObject.FindWithTag ("GameController").GetComponent<GameScript> ().getBoundary ();
		bool topFirst = new System.Random().Next(100)%2 == 0;
		if (topFirst) {
			sendUp ();
		} else {
			sendDown ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.z > spawnBoundary.zMax) 
			sendDown ();
		
		if (transform.position.z < spawnBoundary.zMin) 
			sendUp ();
	}

	void sendDown(){
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.back * SPEED + Vector3.left * (SPEED/2);
	}

	void sendUp(){
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.forward * SPEED + Vector3.left * (SPEED/2);
	}

}
