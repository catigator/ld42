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

	bool IsDirectionFree() {
		Position newPosition = new Position(position.x, position.y);
		newPosition.Add(bc.util.directionPositionDict[direction]);
		return IsFree(newPosition);
	}

	bool IsFree(Position pos) {
		return IsFree(pos.x, pos.y);
	}

	bool IsFree(int x, int y) {

		if ( bc.util.blockList.Contains(bc.gameBoard[x][y])
		|| bc.util.blockList.Contains(bc.objectBoard[x][y]) ) {
			return false;
		}
		
		return true;
	}
}
