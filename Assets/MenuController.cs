using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public GameObject player;
	public BoardController bc;
	public PlayerController pc;
	public GameObject pauseScreen;
	public AudioSource pauseAudio;
	public GameObject imageScreen;
	public GameObject startScreen;
	public GameObject titleScreen;
	public GameObject gameOverScreen;
	public GameObject endScreen;
	public GameObject singleImageScreen;
	public GameObject imagesObject;
	public Text actionText;
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
				// ReturnToGame();
		}

	}

	public void ReturnToGame() {
		GameTime.isPaused = false;
			
		startScreen.SetActive(false);
		bc.BeginLevel();

	}


	public void Restart() {
		SceneManager.LoadScene("Scene1");
	}
}
