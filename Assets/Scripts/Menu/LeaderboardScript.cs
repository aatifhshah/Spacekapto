using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;

public class LeaderboardScript : MonoBehaviour {
	RESTed restfulapi;
	Score[] scores;
	private static int ROWS = 8;
	// Use this for initialization
	void Start () {
		restfulapi = gameObject.AddComponent<RESTed> ();

		SetupHeaders ();

		StartCoroutine (SetupScores ());
	}

	// Update is called once per frame
	void Update () {
		
	}

	// Setup headers
	void SetupHeaders(){
		GameObject.Find ("headers").transform.GetChild (0).transform.GetComponent<PixelFontScript> ().displayStringVal ("NAME");
		GameObject.Find ("headers").transform.GetChild (1).transform.GetComponent<PixelFontScript> ().displayStringVal ("SCORE");
	}


	// Setup Scores
	IEnumerator SetupScores(){
		yield return StartCoroutine(restfulapi.GETScores ());
		scores = restfulapi.scores;

		GameObject scoreBoard = GameObject.Find ("scoreboard");
		for (int i = 0; i < ROWS; i++) {
			SetupRow (scoreBoard.transform.GetChild(i), i);
		}

	}

	//Setup Row
	void SetupRow(Transform row, int indx){
		if(indx < scores.Length && scores.Length > 0){
			Score rowScore = scores [indx];
			row.GetChild (0).transform.GetComponent<PixelFontScript> ().displayStringVal (rowScore.player_name);
			row.GetChild (1).transform.GetComponent<PixelFontScript> ().displayVal (rowScore.player_score);

		}
	}




}

