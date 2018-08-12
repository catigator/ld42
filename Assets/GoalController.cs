using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

	public BoardController bc;

	// Use this for initialization
	void Start () {
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.transform.tag == "Player") {
			if (bc.currentLevel != bc.maxLevel) {
				bc.clearedLevel = bc.currentLevel;
				bc.mc.GoToBetweenLevelsScreen();
			} else {
				bc.mc.GoToEndScreen();
			}
		}
	}
}
