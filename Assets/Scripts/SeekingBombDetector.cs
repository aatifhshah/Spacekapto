using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBombDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		switch (other.tag) {
		default:
			transform.parent.GetComponent<SeekingBOMBScript> ().HitTarget (other.transform);
			break;
		}
	}
}
