using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class RESTed : MonoBehaviour {
	public Score[] scores { get; private set;}
	private static string API_KEY = "<ENTER KEY HERE>";
	private static string GET_URL = "https://baas.kinvey.com/appdata/<ENTER APP ID HERE>/Files/?query={}&sort={\"player_score\":-1}&limit=8";
	private static string POST_URL = "https://baas.kinvey.com/appdata/<ENTER APP ID HERE>/Files";

	//Load all stuff
	public IEnumerator LoadScores(Score[] scores){
		//Coroutine x = StartCoroutine (LoadingAnimation ());
		yield return StartCoroutine(GETScores());
		//StopCoroutine (x);
	}

	// GET all stuff
		public IEnumerator GETScores(){
		UnityWebRequest getScores = new UnityWebRequest (GET_URL);
		getScores.SetRequestHeader ("Authorization", API_KEY);
		getScores.downloadHandler = new DownloadHandlerBuffer ();
		yield return getScores.Send ();
		if (getScores.isError) {
			Debug.Log (getScores.error);
		} else {
			scores = GetScoreArray (getScores.downloadHandler.text);
		}
	}

	// POST new score
	public IEnumerator POSTScore(Score newScore){
		string newScoreJson = JsonUtility.ToJson (newScore);
		byte[] bodyRaw = Encoding.UTF8.GetBytes(newScoreJson);

		using(UnityWebRequest www = UnityWebRequest.Post(POST_URL, "POST")) {
			www.SetRequestHeader ("Authorization",API_KEY);
			www.SetRequestHeader ("Content-Type", "application/json");
			www.uploadHandler = new UploadHandlerRaw (bodyRaw);

			yield return www.Send();

			if(www.isError) {
				Debug.Log(www.error);
			}
			else {
				Debug.Log("New Score Added");
			}
		}
	}

	Score[] GetScoreArray(string json){
		return JsonUtility.FromJson<Wrapper>("{\"array\":" + json + "}").array;
	}
}


[System.Serializable]
public class Wrapper{
	public Score[] array;
}

[System.Serializable]
public class Score{
	public string player_name;
	public int player_score;
}
