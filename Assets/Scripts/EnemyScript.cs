using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	private int health = 1;
	private GameScript gameController;
	private GameObject shot, explosion;
	private Transform shotSpawnPosition;
	private Animations animator;

	void Start()
	{
		GameObject gameControlObj = GameObject.FindWithTag ("GameController");
		animator = gameObject.AddComponent<Animations> ();
		if (gameControlObj != null)
			gameController = gameControlObj.GetComponent<GameScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
		switch (other.tag)
		{
			case "Boundary":
				return;
			case "Shot":
				Destroy (other.gameObject);
				gameController.addScore (gameObject.tag);
                DecrementHealth ();
				StartCoroutine (animator.BlinkAllAnimation (gameObject, gameController.getPrimaryMaterial (), gameController.getSecondaryMaterial ()));
                return;
			case "PlayerShip":
				if(!isBoss())
					DecrementHealth ();
				gameController.decrementLife ();
				return;
			default:
				return;
		}
    }

	bool isBoss(){
		switch (gameObject.tag) {
			case "Enemy_Boss_Fish":
				return true;
			default:
				return false;
		}
	}

	//set health
	public void SetHealth(int h){
		
		if(h > 0)
			health = h;
	}

	void DecrementHealth(){
		health--;
		if (health <= 0)
			Explode ();
	}

	// can shoot
	public void WeaponsFree(GameObject s){
		shotSpawnPosition = gameObject.transform.Find ("shotSpawnPoint").transform;
		shot = s;
		if(shotSpawnPosition != null && shot != null)
			StartCoroutine (FireAtWill ());
	}

	IEnumerator FireAtWill(){
		float ranIntvl = Random.Range (0, 2);
		yield return new WaitForSeconds (ranIntvl);
		Instantiate (shot, shotSpawnPosition.position, shotSpawnPosition.rotation);
	}

	// set explosion
	public void SetExplosion(GameObject e){
		explosion = e;
	}

	// destroy this gameobject with animation
	void Explode(){
		if (explosion != null)
			Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
