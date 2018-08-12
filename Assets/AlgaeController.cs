using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeController : MonoBehaviour {

	public BoardController bc;
	public Position position;
	public Direction direction;
	public bool isFlashing;
	public Direction growDirection;

	public float elapsedTime;
	public float elapsedTimeForChecking;
	

	// Use this for initialization
	void Start () {
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
		isFlashing = false;
		HandleGrowing();
		elapsedTime = 0f;
		elapsedTimeForChecking = 0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		elapsedTimeForChecking += Time.deltaTime;

		if (elapsedTime > 5f) {
			if (isFlashing) {
				GrowAlgae();
				isFlashing = false;
				Animator animator = GetComponent<Animator> ();
    			animator.Play ("Algae");
				elapsedTime = 0f;
			}
		}

		if (elapsedTimeForChecking > 1f) {
			HandleGrowing();
			elapsedTimeForChecking = 0f;
		}
		
	}

	void GrowAlgae() {

		Position newPos = new Position(position.x, position.y);
		newPos.Add(bc.util.directionPositionDict[growDirection]);

		GameObject obj = bc.MakeTile( newPos.x, newPos.y, bc.algaePrefab);
		bc.HandleAlgaeTile(obj, newPos.x, newPos.y, growDirection);

		Direction oppositeDir = bc.util.oppositeDirectionDict[growDirection];
		float rotation = bc.degreesDict[oppositeDir];
		obj.transform.eulerAngles = new Vector3(0,0, rotation);
		
		bc.objectBoard[newPos.x][newPos.y] = TileEnum.Algae;
	}

	void HandleGrowing() {
		if (!isFlashing) {
			elapsedTime = 0f;
			isFlashing = IsAlgaeFree();
			if (isFlashing) {
				Animator animator = GetComponent<Animator> ();
				animator.Play ("AlgaeFlashing");
			}
		}
	}

	bool IsAlgaeFree() {

		bool canGrowForward = true;
		growDirection = direction;

		// 1 - Check that next tile is free
		if (!IsDirectionFree(position, direction)) {
			return false;
		}

		// 2 - Check that that tile is completely free to grow
		Position newPos = new Position(position.x, position.y);
		newPos.Add(bc.util.directionPositionDict[direction]);

		canGrowForward = CheckDirectionGrowPossible(newPos, growDirection);

		// 3 - If can't grow forward, check other directions
		bool canGrowSideways = false;

		List<Direction> sideDirections = GetSideDirections(direction);

		foreach (var dir in sideDirections)
		{
			Position sidePos = new Position(position.x, position.y);
			sidePos.Add(bc.util.directionPositionDict[dir]);

			if (CheckDirectionGrowPossible(sidePos, dir)) {
				growDirection = dir;
				canGrowSideways = true;
			}
		}

		// Return final True / False
		if (!canGrowForward && !canGrowSideways) {
			return false;
		} else {
			return true;
		}

	}

	List<Direction> GetSideDirections(Direction dir) {
		List<Direction> sideDirections = new List<Direction>();

		sideDirections.Add(bc.util.clockwiseDict[dir]);
		sideDirections.Add(bc.util.antiClockwiseDict[dir]);

		return sideDirections;
	}

	bool CheckDirectionGrowPossible(Position pos, Direction growdir) {
		foreach (var dir in bc.util.clockwiseDict.Values)
		{
			if (dir != bc.util.oppositeDirectionDict[growdir]) {
				if (!IsDirectionFree(pos, dir)) {
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
		
		if (!IsInRange(x,y)) {
			return false;
		}

		if ( bc.util.blockList.Contains(bc.gameBoard[x][y])
		|| bc.util.blockList.Contains(bc.objectBoard[x][y]) ) {
			return false;
		}
		// Debug.Log ("ALGAE FREE at " + x.ToString() + " , " + y.ToString());
		return true;
	}

	bool IsInRange(int x, int y) {
		if (x >= 0 & y >= 0) {
			if (x < bc.gameSizeX && y < bc.gameSizeY) {
				return true;
			}
		}
		return false;
	}
}
