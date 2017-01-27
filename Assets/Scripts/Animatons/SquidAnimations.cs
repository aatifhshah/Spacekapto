using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidAnimations : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameScript gameScript = GameObject.FindWithTag ("GameController").GetComponent<GameScript> ();
		Animations animator = gameObject.AddComponent<Animations> ();
		StartCoroutine (animator.BlinkAnimation (gameObject, gameScript.getPrimaryMaterial(), gameScript.getSecondaryMaterial()));
	}
}
