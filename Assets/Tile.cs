using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileEnum {

	None,
	Player,
	Ground,
	GroundBlock,
	GroundDiagonal,
	Algae,
	EvilFish,
	Goal

}; 

public class Tile : MonoBehaviour {

	public TileEnum tileEnum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
