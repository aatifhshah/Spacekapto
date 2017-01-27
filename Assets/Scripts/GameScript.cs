using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {
	private GameObject playerShip, powerupsDisplay, scoreDisplay, powerupsTypeDisplay;
	private bool isVulnerable, powerupShootable;
	private PixelFontScript powerupDisplayController, scoreDisplayController;
	private int playerLives,playerScore,playerPowerups;
	private static int INVULNERABLE_TIME = 2;
	public GameObject[] heartsDisplayObjects;
	public GameObject shotExplosion, gameOver;
	public Boundary boundary,FishBoundary;
	public Material primary, secondary;



	// Use this for initialization
	void Start () {
		playerShip = GameObject.Find ("PlayerShip");
		powerupsDisplay = GameObject.Find ("PowerupsBoard");
		scoreDisplay = GameObject.Find ("ScoreBoard");
		powerupsTypeDisplay = GameObject.FindWithTag ("Powerup_Type");

		playerLives = 3;
		playerScore = 0;
		playerPowerups = 3;

		isVulnerable = true;
        powerupShootable = true;

		powerupDisplayController = powerupsDisplay.GetComponent<PixelFontScript> ();
		scoreDisplayController = scoreDisplay.GetComponent<PixelFontScript> ();

		//Display ScoreBoard
		scoreDisplayController.displayVal(playerScore);
		//Display Powerups
		powerupDisplayController.displayVal(playerPowerups);

		//make invulenerable on start
		StartCoroutine(startInvulnerability());
	}
		
	// Update is called once per frame
	void Update () {
		
	}

	// Happens when player runs out of lives OR kills all of em evil aliens
	public void GameOver(string title)
	{
		//Destroy Player Ship x(
		Destroy(playerShip);
		GameObject gOver = Instantiate (gameOver, gameOver.transform.position, gameOver.transform.rotation);
		gOver.transform.GetChild (0).GetComponent<TextMesh> ().text = title;

	}

	public int getScore(){
		return playerScore;
	}

	// Life lost
	public void decrementLife()
	{
		if (isVulnerable) {
			Vector3 newPos; 
			playerLives -= 1;
			if (playerLives <= 0) {
				GameOver ("gameover");
				newPos = heartsDisplayObjects [0].transform.position;
				newPos.y -= 7;
				heartsDisplayObjects [0].transform.position = newPos;
			} else {
				newPos = heartsDisplayObjects [playerLives].transform.position;
				newPos.y -= 7;
				heartsDisplayObjects [playerLives].transform.position = newPos;
			}

			StartCoroutine (startInvulnerability ());
		}
	}

	// TODO tightly coupled
	IEnumerator startInvulnerability(){
		isVulnerable = false;
		yield return StartCoroutine (playerShip.GetComponent<PlayerScript> ().isInvulnerable (INVULNERABLE_TIME));
		isVulnerable = true;
	}

	// Life gained
	public void addLife()
	{
		playerLives += 1;
		Vector3 newPos;
		if (playerLives > 5) {
			playerLives = 5;
		} else {
			newPos = heartsDisplayObjects [playerLives].transform.position;
			newPos.y += 7;
			heartsDisplayObjects [playerLives].transform.position = newPos;
		}
	}

	//Add Score
	public void addScore(string enemyType){
		playerScore += EnemyValues.getVal(enemyType);
		scoreDisplayController.displayVal (playerScore);
	}

	//Returns Playable Boundary for game
	public Boundary getBoundary(){
		return boundary;
	}

	public Boundary getFishBoundary(){
		return FishBoundary;
	}
		
	//Set Powerup type
	public void setPowerupDisplay(GameObject p){
		GameObject x = Instantiate(p, powerupsTypeDisplay.transform.position, Quaternion.identity);
        x.transform.SetParent(powerupsTypeDisplay.transform);
        x.tag = "Untagged";
    }

    public void incrementPowerup()
    {
        playerPowerups++;
        powerupDisplayController.displayVal(playerPowerups);
    }

    public bool hasPowerup()
    {
        if (playerPowerups > 0 && powerupShootable)
        {
            StartCoroutine(powerupTimer());
			playerPowerups--;
			powerupDisplayController.displayVal(playerPowerups);
            return true;
        }
		return false;
    }

    IEnumerator powerupTimer()
    {
        powerupShootable = false;
        yield return new WaitForSeconds(2.0f);
        powerupShootable = true;
    }

	//Get Primary Material
	public Material getPrimaryMaterial(){return primary;}
	//Get Secondary Material
	public Material getSecondaryMaterial(){return secondary;}

	//Get shot Explosion
	public GameObject getShotExplosion(){return shotExplosion;}

	////Set Theme
	// set playership
	// set shot
	// set enemy shot
	// set all enemies
	// set info bar

}


// Enemy score values
[System.Serializable]
static class EnemyValues
{
	public static int getVal(string enemyType){
		switch (enemyType) {
		case "Enemy_Jelly":
			return JELLY;
		case "Enemy_BatRay":
			return BATRAY;
		case "Enemy_Squid":
			return SQUID;
		case "Enemy_Dolphin":
			return DOLPHIN;
        case "Enemy_Boss_Fish":
            return FISH;
		default:
			return 0;
		}
	}

	public static int JELLY { get { return 10; } }
	public static int BATRAY { get { return 15; } }
	public static int SQUID { get { return 20; } }
	public static int DOLPHIN { get { return 25; } }
    public static int FISH { get { return 250; } }
}

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax, y;
}

[System.Serializable]
public enum Powerups{
	SeekingBOMB, Incinerator, Lazer
}

[System.Serializable]
public enum Explosions{
	Normal
}
