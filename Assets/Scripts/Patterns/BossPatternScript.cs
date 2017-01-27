using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternScript : MonoBehaviour
{
    private Boundary spawnBoundary;
    private static int SPEED = 5;

    // Use this for initialization
    void Start()
    {
        spawnBoundary = GameObject.FindWithTag("GameController").GetComponent<GameScript>().getFishBoundary();
		StartCoroutine (StartPattern ());
	}

    // Update is called once per frame
    void Update()
    {
        if (spawnBoundary.zMax  < transform.position.z)
            goDown();
        if (spawnBoundary.zMin > transform.position.z)
            goUp();
    }

    void goUp()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward * SPEED;
    }

    void goDown()
    {
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.back * SPEED;
    }

    void goLeft()
    {

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.left * SPEED;
    }

    void goRight()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * SPEED;
    }

    IEnumerator StartPattern()
    {
		while (true) {
			goLeft ();
			while (spawnBoundary.xMin < transform.position.x)
				yield return new WaitForEndOfFrame ();
			goRight ();
			while (spawnBoundary.xMax > transform.position.x)
				yield return new WaitForEndOfFrame ();
			goUp ();
			yield return new WaitForSeconds (Random.Range (4, 6));
		}
    }


}
