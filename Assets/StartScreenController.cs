using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenEnum {
	Start,
	Title,
	BetweenLevels,
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
	public GameObject middleText;

	public string newContinueText;
	
	public ScreenEnum screenEnum;

	// Use this for initialization
	void Start () {
		if (ogText == "") {
			ogText = startText.text;
		}
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
		}  else if (screenEnum == ScreenEnum.BetweenLevels) {
			SpaceBetweenLevels();
		}
	}

	public void SpaceBetweenLevels() {
		mc.ReturnToGame();
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

	public void ResetText() {
		if (ogText == "") {
			ogText = startText.text;
		}
		startText.text = "";
		continueText.GetComponent<Text>().text = "";
		currentChar = 0;
		elapsedTime = 0f;
		middleText.SetActive(false);
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
				middleText.SetActive(true);
				if (!continueText.activeSelf) {
					continueText.SetActive(true);
				}
				continueTextText.text = newContinueText.Replace("\\n","\n");
			}
		}
	}
}
