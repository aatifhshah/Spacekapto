using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameScript gameScript = GameObject.FindWithTag ("GameController").GetComponent<GameScript> ();
		Animations animator = gameObject.AddComponent<Animations> ();
		StartCoroutine(Explode(animator, gameScript));
	}

	IEnumerator Explode(Animations animator, GameScript gameScript){
		yield return StartCoroutine(animator.ExplosionAnimation (gameObject, gameScript.getPrimaryMaterial (), gameScript.getSecondaryMaterial ()));
		Destroy (gameObject);
	}

}
