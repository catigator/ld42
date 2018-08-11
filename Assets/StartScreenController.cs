using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenEnum {
	Start,
	Title,
	End,
	Menu,
	GameOver
}

public class StartScreenController : MonoBehaviour {

	public Text startText;
	public string ogText;

	public float typeSpeed;
	public float ogTypeSpeed;
	public float elapsedTime;

	public int currentChar;

	public AudioSource blipAudio;
	public MenuController mc;

	public GameObject continueText;
	public Text continueTextText;

	public string newContinueText;
	
	public ScreenEnum screenEnum;

	// Use this for initialization
	void Start () {
		ogText = startText.text;
		ogTypeSpeed = typeSpeed;
		startText.text = "";
		elapsedTime = 0f;
		blipAudio = GetComponent<AudioSource>();
		continueTextText = continueText.GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (currentChar > ogText.Length -2) {
				HandleSpace();
			} else {
				currentChar = ogText.Length;
				startText.text = ogText;
			}
		} else if (Input.anyKey) {
			typeSpeed = ogTypeSpeed/3f;
		} else {
			typeSpeed = ogTypeSpeed;
		}

		if (currentChar > ogText.Length -2) {
			HandleInput();
		}

		AdvanceText();
		
	}

	public void HandleSpace() {
		Debug.Log("HabdleSpace");
		if (screenEnum == ScreenEnum.Start) {
			SpaceStart();
		} else if (screenEnum == ScreenEnum.End) {
			mc.ReturnToGame();
		} else if (screenEnum == ScreenEnum.GameOver) {
			// SpaceGameOver();
		}
	}

	public void HandleInput() {
		if (screenEnum == ScreenEnum.End ||
				screenEnum == ScreenEnum.GameOver) {
			
			if (Input.GetKeyDown(KeyCode.Q)) {
				Debug.Log("Quitting");
				Application.Quit();
			}
		} if (screenEnum == ScreenEnum.End) {
			
			if (Input.GetKeyDown(KeyCode.N)) {
				Debug.Log("Restart");
				mc.Restart();
			}
		} 
	}

	public void SpaceStart() {
		mc.ReturnToGame();
	}

	public void SpaceEnd() {
		mc.Restart();
	}

	public void SpaceGameOver() {
		mc.Restart();
	}


	public void AdvanceText() {
		if (elapsedTime > typeSpeed) {
			elapsedTime -= typeSpeed;

			if (currentChar < ogText.Length) {
				currentChar += 1;
				
				string newText = ogText.Substring(0, Mathf.Min(currentChar, ogText.Length));
				// Debug.Log(newText);
				startText.text = newText;
				if (ogText[currentChar-1] != ' ') {
					blipAudio.Play();
				}
			} else {
				if (!continueText.activeSelf) {
					continueText.SetActive(true);
				}
				continueTextText.text = newContinueText.Replace("\\n","\n");
			}
		}
	}
}
