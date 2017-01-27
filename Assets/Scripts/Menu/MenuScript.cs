using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Timer for restart and menu for the game
public class MenuScript : MonoBehaviour {
	public Material White, Black, Grey;
	public GameObject Background;
	public GameObject[] MenuOptions;


	// Use this for initialization
	void Start () {
		StartCoroutine (DisplayMenu ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameOver(int score){
		Background.GetComponent<Renderer>().material = White;
		//bring scoreDisplay and countdowndisplay up
		//scoreDisplayController.display(score);
		StartCoroutine(CountdownBeforeRedirect());
	}

	public void MainMenu(){
		Background.GetComponent<Renderer>().material = Black;
		//DisplayOptions();
		//bring options up
	}

	IEnumerator CountdownBeforeRedirect(){
		int time = 6;
		while (time >= 0) {
			//countDownDisplayController.display(time);
			time--;
			yield return new WaitForSeconds(1);
		}
	}

	IEnumerator DisplayMenu(){
		yield return new WaitForSeconds (1);
		foreach (GameObject optn in MenuOptions) {
			Transform spawnPos = optn.transform.GetComponent<ButtonScript>().getMarkers().source.transform;
			Instantiate(optn, spawnPos.position, optn.transform.rotation);
			yield return new WaitForSeconds (1);
		}
	}
		
	public Material getTransitionMaterial(){
		return Grey;
	}

	public Material getSecondaryMaterial(){
		return White;
	}

	void ResetMenu(){
	}
}

[System.Serializable]
public class Option{
	public GameObject source, midenter, mid, midexit, exit;
}

public enum Button{
	Play,Leaderboard,Help
}
