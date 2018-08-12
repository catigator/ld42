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
				ReturnToGame();
		}

		if (Input.GetKeyDown(KeyCode.T)) {
				GoToStartScreen();
		}

		if (Input.GetKeyDown(KeyCode.Y)) {
				GoToBetweenLevelsScreen();
		}

	}

	public void ReturnToGame() {
		KillAllTiles();
		GameTime.isPaused = false;
			
		startScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		betweenLevelsScreen.SetActive(false);
		gameScreen.SetActive(true);
		bc.BeginLevel(bc.currentLevel + 1);

	}

	public void GoToStartScreen() {
		KillAllTiles();
		GameTime.isPaused = true;
		gameScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		betweenLevelsScreen.SetActive(false);
		startScreen.GetComponent<StartScreenController>().ResetText();
		startScreen.SetActive(true);
	}

	public void GoToBetweenLevelsScreen() {
		KillAllTiles();
		GameTime.isPaused = true;
		gameScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		betweenLevelsScreen.GetComponent<StartScreenController>().ResetText();
		startScreen.SetActive(false);
		betweenLevelsScreen.SetActive(true);
	}

	public void GoToGameOverScreen() {
		KillAllTiles();
		GameTime.isPaused = true;
		gameScreen.SetActive(false);
		gameOverScreen.GetComponent<StartScreenController>().ResetText();
		startScreen.SetActive(false);
		betweenLevelsScreen.SetActive(false);
		gameOverScreen.SetActive(true);

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
