using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScript : MonoBehaviour {
	public Material grey, white;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		SceneManager.LoadScene ("Menu");
	}

	void OnMouseOver(){
		GetComponent<TextMesh>().color = grey.color;
	}

	void OnMouseExit(){
		GetComponent<TextMesh>().color = white.color;
	}

}
