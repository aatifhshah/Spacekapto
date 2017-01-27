using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TightWaveyPatternScript : MonoBehaviour {
	private Boundary spawnBoundary;
	private static int SPEED = 5;
	private static float DELAY = 0.7f;

	// Use this for initialization
	void Start () {
		spawnBoundary = GameObject.FindWithTag ("GameController").GetComponent<GameScript> ().getBoundary ();
		StartCoroutine (startPattern ());
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < spawnBoundary.zMin)
			goUp ();
		if (transform.position.z > spawnBoundary.zMax)
			goDown ();
		
	}

	IEnumerator startPattern(){
		while (true) {
			goUp ();
			yield return new WaitForSeconds (DELAY);
			goDown ();
			yield return new WaitForSeconds (DELAY);
		}
	}

	void goStraight(){
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.left * SPEED;
	}

	void goUp(){
		gameObject.GetComponent<Rigidbody> ().velocity = (Vector3.forward + Vector3.left) * SPEED;
	}

	void goDown(){
		gameObject.GetComponent<Rigidbody> ().velocity = (Vector3.back + Vector3.left) * SPEED;
	}
}
