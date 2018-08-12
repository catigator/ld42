using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeController : MonoBehaviour {

	public BoardController bc;
	public Position position;
	public Direction direction;
	public bool isFlashing;

	public float elapsedTime;
	

	// Use this for initialization
	void Start () {
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
		isFlashing = false;
		HandleGrowing();
		
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		
	}

	void HandleGrowing() {

		isFlashing = IsAlgaeFree();
		if (isFlashing) {
			Animator animator = GetComponent<Animator> ();
    		animator.Play ("AlgaeFlashing");
		}
	}

	bool IsAlgaeFree() {

		// 1 - Check that next tile is free
		if (!IsDirectionFree(position, direction)) {
			return false;
		}

		// 2 - Check that that tile is completely free to grow
		Position newPos = new Position(position.x, position.y);
		newPos.Add(bc.util.directionPositionDict[direction]);

		foreach (var dir in bc.util.clockwiseDict.Values)
		{
			if (dir != bc.util.oppositeDirectionDict[direction]) {
				if (!IsDirectionFree(newPos, dir)) {
					return false;
				}
			}
		}

		return true;
	}



	bool IsDirectionFree(Position pos, Direction dir) {
		Position newPosition = new Position(pos.x, pos.y);
		newPosition.Add(bc.util.directionPositionDict[dir]);
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
		// Debug.Log ("ALGAE FREE at " + x.ToString() + " , " + y.ToString());
		return true;
	}
}
