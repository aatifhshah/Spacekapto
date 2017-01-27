using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverScript : MonoBehaviour {
	public InputField inptF;
	public TextMesh overScreenTitle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			//Post new score
			Score newscore = new Score();
			string name = inptF.text;
			newscore.player_score = GameObject.Find ("Game").GetComponent<GameScript> ().getScore ();
			if (!name.Equals ("INPUT PILOT ID")) {
				newscore.player_name = name;
			} else {
				newscore.player_name = "UND";
			}
			StartCoroutine (postit(newscore));
		}
	}

	IEnumerator postit(Score newscore){
		RESTed api = gameObject.AddComponent<RESTed> ();
		yield return StartCoroutine(api.POSTScore (newscore));
		SceneManager.LoadScene ("Menu");
	}


}
