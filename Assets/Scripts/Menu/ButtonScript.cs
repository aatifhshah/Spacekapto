using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {
	public Button label;
	public Option markers;
	private Material grey,white;
	private GameObject MenuObj;
	private PixelFontScript PixlScript;
	private bool enter, exit;
	// Use this for initialization
	void Start () {
		PixlScript = gameObject.transform.GetComponent<PixelFontScript> ();
		MenuObj = GameObject.Find ("Menu");
		grey = MenuObj.GetComponent<MenuScript> ().getTransitionMaterial ();
		white = MenuObj.GetComponent<MenuScript> ().getSecondaryMaterial ();
		PixlScript.displayStringVal (getButtonString(label));

		enter = true;
		exit = false;
	}

	// Update is called once per frame
	void Update () {
		if (enter) {
			transform.position = Vector3.Lerp (transform.position, markers.mid.transform.position, Time.deltaTime * 2.5f);
		}

		if (Vector3.Distance (transform.position, markers.mid.transform.position) < 0.1f)
			enter = false;

		if (exit) {
			StartCoroutine(executeLabel (label));
			transform.position = Vector3.Lerp (transform.position, markers.exit.transform.position, Time.deltaTime * 1.5f);
		}

	}

	void OnMouseDown(){
		StartCoroutine (StartExit());
	}

	void OnMouseOver(){
		PixlScript.redrawWithMaterial (grey, getButtonString(label));
	}

	void OnMouseExit(){
		PixlScript.redrawWithMaterial (white, getButtonString (label));
	}

	IEnumerator StartExit(){
		yield return StartCoroutine (PixlScript.blinkMaterial (grey, getButtonString(label)));
		exit = true;
	}

	string getButtonString(Button label){
		
		switch (label) {
		case Button.Play:
			return "PLAY";
		case Button.Leaderboard:
			return "LEADERBOARD";
		case Button.Help:
			return "HELP";
		default:
			Debug.Log ("here");
			return null;
		}
	}

	IEnumerator executeLabel(Button label){
		yield return new WaitForSeconds (0.5f);
		switch (label) {
		case Button.Play:
			SceneManager.LoadScene ("Main");
			break;
		case Button.Leaderboard:
			SceneManager.LoadScene ("Leaderboard");
			break;
		case Button.Help:
			SceneManager.LoadScene ("Help");
			break;
		}
	}

	public Option getMarkers(){
		return markers;
	}
}
