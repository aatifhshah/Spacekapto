using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {
	public float zMin, zMax, xSpawn;
	public GameObject[] bgObstacles;
	private static int DURATION = 3;

	// Use this for initialization
	void Start () {
		StartCoroutine (bgSpawner ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator bgSpawner(){
		while (true) {
			yield return StartCoroutine (Spawner ());
		}
	}

	IEnumerator Spawner(){
		foreach (GameObject obj in bgObstacles) {
			int randomQty = Random.Range (70, 120);
			yield return StartCoroutine (SpawnOBS(obj, randomQty));
		}
	}

	IEnumerator SpawnOBS(GameObject obj, int qty){
		float interval = DURATION / qty;
		for (int i = 0; i < qty; i++) {
			Transform clone = (Transform) Instantiate(obj.transform, new Vector3(xSpawn, 0.5f , Random.Range(zMin,zMax)), obj.transform.rotation);
			clone.GetComponent<Rigidbody> ().velocity = transform.right * 8;
			yield return new WaitForSeconds (interval);
		}
	}
}
