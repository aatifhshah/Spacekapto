using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameScript gameScript = GameObject.FindWithTag ("GameController").GetComponent<GameScript> ();
		Animations animator = gameObject.AddComponent<Animations> ();
		StartCoroutine (animator.MultipleAnimation (gameObject, gameScript.getPrimaryMaterial(), gameScript.getSecondaryMaterial()));
	}
}
