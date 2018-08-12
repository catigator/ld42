using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public GameObject player;
	public BoardController bc;
	public PlayerController pc;
	public GameObject startScreen;
	public GameObject titleScreen;
	public GameObject gameOverScreen;
	public GameObject endScreen;
	public GameObject gameScreen;
	public GameObject betweenLevelsScreen;


	public AudioManager am;
// 
	public GameObject imagePrefab;
	public List<Texture2D> images;

	public Color greenColor;
	public Color redColor;
	public Color whiteColor;

	// Use this for initialization
	void Start () {
		GameTime.isPaused = true;
		titleScreen.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M)) {
			GameTime.isPaused = !GameTime.isPaused;
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			bc.currentLevel -= 1;
			ReturnToGame();
		}

		// if (Input.GetKeyDown(KeyCode.T)) {
		// 	GoToStartScreen();
		// }

		// if (Input.GetKeyDown(KeyCode.Y)) {
		// 	GoToBetweenLevelsScreen();
		// }

		// if (Input.GetKeyDown(KeyCode.U)) {
		// 	GoToEndScreen();
		// }

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.N)) {
			if (bc.clearedLevel > bc.currentLevel
			&& bc.currentLevel < bc.maxLevel) {
				ReturnToGame();
			} else {
				bc.am.cant.Play();
			}
		}

		if (Input.GetKeyDown(KeyCode.P)) {
			if (bc.currentLevel > 1) {
				bc.currentLevel -= 2;
				ReturnToGame();
			} else {
				bc.am.cant.Play();
			}
		}



	}

	public void ReturnToGame() {
		KillAllTiles();
		GameTime.isPaused = false;
			
		startScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		endScreen.SetActive(false);
		betweenLevelsScreen.SetActive(false);
		gameScreen.SetActive(true);
		bc.BeginLevel(bc.currentLevel + 1);

	}

	public void GoToStartScreen() {
		KillAllTiles();
		GameTime.isPaused = true;
		gameScreen.SetActive(false);
		endScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		betweenLevelsScreen.SetActive(false);
		startScreen.GetComponent<StartScreenController>().ResetText();
		startScreen.SetActive(true);
	}

	public void GoToBetweenLevelsScreen() {
		KillAllTiles();
		GameTime.isPaused = true;
		gameScreen.SetActive(false);
		endScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		betweenLevelsScreen.GetComponent<StartScreenController>().ResetText();
		startScreen.SetActive(false);
		betweenLevelsScreen.SetActive(true);
	}

	public void GoToGameOverScreen() {
		KillAllTiles();
		GameTime.isPaused = true;
		gameScreen.SetActive(false);
		endScreen.SetActive(false);
		gameOverScreen.GetComponent<StartScreenController>().ResetText();
		startScreen.SetActive(false);
		betweenLevelsScreen.SetActive(false);
		gameOverScreen.SetActive(true);

	}

	public void GoToEndScreen() {
		KillAllTiles();
		GameTime.isPaused = true;
		gameScreen.SetActive(false);
		endScreen.GetComponent<StartScreenController>().ResetText();
		startScreen.SetActive(false);
		betweenLevelsScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		endScreen.SetActive(true);
	}

	public void KillAllTiles() {
		foreach (Transform child in bc.tilesObject.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}


	public void Restart() {
		SceneManager.LoadScene("Scene1");
	}
}
