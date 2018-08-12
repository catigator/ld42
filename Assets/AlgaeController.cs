using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeController : MonoBehaviour {

	public BoardController bc;
	public Position position;
	public Direction direction;

	// Use this for initialization
	void Start () {
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
