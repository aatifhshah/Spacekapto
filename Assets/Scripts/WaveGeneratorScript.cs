using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

public class WaveGeneratorScript : MonoBehaviour {
	private static int ENEMY_SPEED = 5;
	private static int WAVE_DELAY = 1;
	private Level currentLevel;
	private Boundary spawnBoundary;
	private GameObject gameController, playerController;

	public GameObject defaultExplosion;
	public List<Level> Levels;


	void Start()
	{
		gameController = GameObject.FindWithTag ("GameController");
		playerController = GameObject.FindWithTag ("PlayerShip");
		spawnBoundary = gameController.GetComponent<GameScript> ().getBoundary ();
		//foreach level, start spawner
		StartCoroutine(StartGame());
	}

	IEnumerator StartGame(){
		foreach (Level level in Levels) {
			currentLevel = level;
			gameController.GetComponent<GameScript> ().setPowerupDisplay (level.levelPowerup.transform.GetChild(0).gameObject);
			playerController.GetComponent<PlayerScript> ().setPowerup (level.levelPowerup);
			yield return StartCoroutine (SpawnLevel (level));
		}
		gameController.GetComponent<GameScript> ().GameOver ("   You  won!");

	}

	IEnumerator SpawnLevel(Level level)
	{
		Wave[] waves = level.waves.ToArray ();
		for (int i = 0; i < waves.Length - 1; i++) {
			StartCoroutine (SpawnWave (waves[i]));
			yield return new WaitForSeconds (waves[i].duration);
		}
		yield return StartCoroutine(SpawnBossAction(waves[waves.Length - 1]));
	}

	IEnumerator SpawnWave(Wave wave){
		foreach (WaveAction action in wave.actions) {
			StartCoroutine (SpawnAction (action, wave.duration));
		}
		yield break;
	}

	IEnumerator SpawnAction(WaveAction action, float levelDuration){
		float spawnInterval = (levelDuration - WAVE_DELAY) / action.quantity;
		yield return new WaitForSeconds (WAVE_DELAY);
		if (action.body != null && action.quantity > 0) {
			for (int i = 0; i < action.quantity; i++) {
				Vector3 spawnPosition = getSpawnPosition(action.position);
				Transform clone = (Transform) Instantiate(action.body, spawnPosition, transform.rotation);
				setupClone (action, clone);
				yield return new WaitForSeconds (spawnInterval);
			}
		}
	}

	IEnumerator SpawnBossAction(Wave bossWave){
		WaveAction action = bossWave.actions [0];
		Transform clone = null;
		if (action.body != null && action.quantity > 0) {
			for (int i = 0; i < action.quantity; i++) {
				Vector3 spawnPosition = getSpawnPosition(action.position);
				clone = (Transform) Instantiate(action.body, spawnPosition, transform.rotation);
				setupClone (action, clone);
			}
		}
		yield return StartCoroutine (waitForDeath (clone));
	}

	IEnumerator waitForDeath(Transform clone){
		while (clone != null) {
			yield return new WaitForEndOfFrame ();
		}
	}

	//Setup Clone
	void setupClone(WaveAction action, Transform clone){
		
		setSpawnPattern (action, action.pattern, clone);

		if (clone.GetComponent<EnemyScript> () != null) {
			clone.GetComponent<EnemyScript> ().SetHealth (action.health);

			if (action.shot != null) {
				clone.GetComponent<EnemyScript> ().WeaponsFree (action.shot);
			}

			if (action.customExplosion != null)
				clone.GetComponent<EnemyScript> ().SetExplosion (action.customExplosion);
			else 
				clone.GetComponent<EnemyScript>().SetExplosion(defaultExplosion);
		}

	}

	/////SPAWN PATTERNS

	/// Pattern Setter
	void setSpawnPattern(WaveAction action, SpawnPattern pattern, Transform clone){
		switch (pattern) {
		case SpawnPattern.Linear:
			LinearPattern (clone, action.linearspeed);
			break;
		case SpawnPattern.Wavey:
			clone.gameObject.AddComponent<WaveyPatternScript>();
			break;
		case SpawnPattern.CrossUpwards:
			clone.gameObject.AddComponent<CrossPatternScript> ().sendUpwards ();
			break;
		case SpawnPattern.CrossDownwards:
			clone.gameObject.AddComponent<CrossPatternScript> ().sendDownwards ();
			break;
		case SpawnPattern.TightWavey:
			clone.gameObject.AddComponent<TightWaveyPatternScript> ();
			break;
		case SpawnPattern.Boss:
			clone.gameObject.AddComponent<BossPatternScript> ();
			break;
		default:
			LinearPattern (clone, ENEMY_SPEED);
			break;
		}
	}
		
	/// Linear Pattern
	void LinearPattern(Transform body, int speed){
		body.GetComponent<Rigidbody> ().velocity = Vector3.left * speed;
	}

	/// Get Spawn Position
	Vector3 getSpawnPosition(SpawnPosition spawnPosition){
		switch (spawnPosition){
		case SpawnPosition.Top:
			return new Vector3(spawnBoundary.xMax + 1, spawnBoundary.y, spawnBoundary.zMax - 0.5f);
		case SpawnPosition.Middle:
			return new Vector3(spawnBoundary.xMax + 1, spawnBoundary.y, spawnBoundary.zMin + (spawnBoundary.zMax + Math.Abs(spawnBoundary.zMin))/2);
		case SpawnPosition.Bottom:
			return new Vector3(spawnBoundary.xMax + 1, spawnBoundary.y, spawnBoundary.zMin);
		case SpawnPosition.Random:
			return new Vector3(spawnBoundary.xMax + 1, spawnBoundary.y, UnityEngine.Random.Range(spawnBoundary.zMin, spawnBoundary.zMax));
		default:
			return new Vector3(spawnBoundary.xMax + 1, spawnBoundary.y, UnityEngine.Random.Range(spawnBoundary.zMin, spawnBoundary.zMax));
		}
	}

	// Get Current Level Info
	public Level getCurrLevel(){
		return currentLevel;
	}

}

[System.Serializable]
public class Level{
	public string name;
	public float duration;
	public GameObject levelPowerup;
	public List<Wave> waves;
}

[System.Serializable]
public class Wave {
	public string name;
	public float duration;
	public List<WaveAction> actions;
}
	
[System.Serializable]
public class WaveAction {
	public string name;
	public Transform body;
	public SpawnPattern pattern;
	public SpawnPosition position;
	public int quantity;
	public int health;
    public int linearspeed;
	public GameObject shot;
	public GameObject customExplosion;
}
			
public enum SpawnPattern {
	Linear, CrossUpwards, CrossDownwards, Wavey, Seeking, TightWavey, AI, Boss
}

public enum SpawnPosition {
	Top, Middle, Bottom, Random
}



