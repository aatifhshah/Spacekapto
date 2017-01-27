using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPatternScript : MonoBehaviour {
	private Boundary spawnBoundary;
	private static int SPEED = 5;
	private static float CROSS_DELAY = 0.5f;

	// Use this for initialization
	void Start () {
		spawnBoundary = GameObject.FindWithTag ("GameController").GetComponent<GameScript> ().getBoundary ();
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.z > spawnBoundary.zMax || transform.position.z < spawnBoundary.zMin)
			goStraight ();
	}

	public void sendDownwards(){
		StartCoroutine (headDownwards ());
	}

	public void sendUpwards(){
		StartCoroutine (headUpwards ());
	}

	IEnumerator headUpwards(){
		goStraight ();
		yield return new WaitForSeconds (CROSS_DELAY);
		goUp ();
	}

	IEnumerator headDownwards(){
		goStraight ();
		yield return new WaitForSeconds (CROSS_DELAY);
		goDown ();
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
