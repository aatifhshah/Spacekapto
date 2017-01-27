using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	private Rigidbody playerBody;
	private Boundary boundary;
	private bool canShoot = true;
	private static float SPEED = 0.11f;
	private static float CANSHOOT_DELAY = 0.2f;
	public GameObject shot, invulnerableObj; 
	private GameObject gameController, powerup;
    public Transform shotSpawnPoint;




	void Start()
	{
		gameController = GameObject.FindWithTag ("GameController");
		boundary = gameController.GetComponent<GameScript> ().getBoundary ();
		playerBody = GetComponent<Rigidbody>();
	}

    void Update()
    {
		// Normal shot
		if (Input.GetKeyDown (KeyCode.Space) && canShoot) {
			Instantiate (shot, shotSpawnPoint.position, shotSpawnPoint.rotation);
			StartCoroutine (ShotTimer ());
		}
       
		// Powerup shot
		if ((Input.GetKeyDown (KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.RightShift))) && gameController.GetComponent<GameScript> ().hasPowerup())
            Instantiate(powerup, shotSpawnPoint.position, shotSpawnPoint.rotation);


		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			Vector3 newPos = playerBody.position;
			newPos.z += SPEED;
			playerBody.position = newPos;
		}

		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			Vector3 newPos = playerBody.position;
			newPos.z -= SPEED;
			playerBody.position = newPos;
		}

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			Vector3 newPos = playerBody.position;
			newPos.x += SPEED;
			playerBody.position = newPos;
		}

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			Vector3 newPos = playerBody.position;
			newPos.x -= SPEED;
			playerBody.position = newPos;
		}
    }

    void FixedUpdate()
    {
		//playerBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * SPEED;
		playerBody.position = new Vector3(Mathf.Clamp(playerBody.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(playerBody.position.z, boundary.zMin, boundary.zMax));
    }

	void OnTriggerEnter(Collider other){
		switch (other.tag) {
			case "EnemyShot":
                Destroy(other.gameObject);
				gameController.GetComponent<GameScript> ().decrementLife ();
				break;
			case "Enemy_Fish_Boss":
				gameController.GetComponent<GameScript> ().decrementLife ();
				break;
		}
	}

	//Set Powerup type
	public void setPowerup(GameObject obj){
		powerup = obj;
	}

	//invulnerable
	public IEnumerator isInvulnerable(float duration){
		GameObject temp = Instantiate (invulnerableObj, transform.position, Quaternion.identity);
		temp.transform.parent = gameObject.transform;
		yield return new WaitForSeconds(duration);
		Destroy (temp);
	}

	IEnumerator ShotTimer(){
		canShoot = false;
		yield return new WaitForSeconds (CANSHOOT_DELAY);
		canShoot = true;
	}


}




