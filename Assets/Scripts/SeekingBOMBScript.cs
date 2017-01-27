using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBOMBScript : MonoBehaviour {
	private static int SPEED = 4;
	private bool targetFound = false;
	private Transform target;


	// Use this for initialization
	void Start () {
		StartMovement ();
	}
	
	// Update is called once per frame
	void Update () {
		if (targetFound && target != null && target.transform.position.x > gameObject.transform.position.x){
			transform.position = Vector3.MoveTowards (transform.position, target.position, SPEED * Time.deltaTime);
		}	
	}

	void StartMovement(){
		gameObject.GetComponent<Rigidbody> ().velocity = transform.right * SPEED;
	}

	public void HitTarget(Transform enemy){
		targetFound = true;
		target = enemy;					
	}
}
