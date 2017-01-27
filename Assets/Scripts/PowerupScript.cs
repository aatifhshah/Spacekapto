using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {
	private GameScript gameController;
	// Use this for initialization
	void Start () {
		GameObject gameControlObj = GameObject.FindWithTag ("GameController");

		if (gameControlObj != null)
			gameController = gameControlObj.GetComponent<GameScript> ();
	}
	
	void OnTriggerEnter(Collider other){
		switch (other.tag) {
			case "PlayerShip":
				gameController.incrementPowerup ();
				Destroy (gameObject);
				return;
			case "Shot":
				return;
			default:
				return;
		}
	}
}
