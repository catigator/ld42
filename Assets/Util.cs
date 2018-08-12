using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour {

	public Dictionary<Direction, Direction> oppositeDirectionDict; 
	public Dictionary<Direction, Position> directionPositionDict; 
	public Dictionary<Direction, Direction> clockwiseDict;
	public Dictionary<Direction, Direction> antiClockwiseDict; 

	public List<TileEnum> blockList;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InitDicts() {
		InitOppositeDirectionDict();
		InitDirectionPositionDict();		
		InitClockwiseDict();
		InitAntiClockwiseDict();
		InitBlockList();


	}

	public void InitOppositeDirectionDict() {
		oppositeDirectionDict = new Dictionary<Direction, Direction> ();

		oppositeDirectionDict[Direction.Down] = Direction.Up;
		oppositeDirectionDict[Direction.Left] = Direction.Right;
		oppositeDirectionDict[Direction.Up] = Direction.Down;
		oppositeDirectionDict[Direction.Right] = Direction.Left;

		oppositeDirectionDict[Direction.None] = Direction.None;

	}

	public void InitDirectionPositionDict() {
		directionPositionDict = new Dictionary<Direction, Position> ();

		directionPositionDict[Direction.Down] = new Position(0, -1);
		directionPositionDict[Direction.Left] = new Position(-1, 0);
		directionPositionDict[Direction.Up] = new Position(0, 1);
		directionPositionDict[Direction.Right] = new Position(1, 0);

		directionPositionDict[Direction.None] = new Position(0, 0);

	}

	public void InitClockwiseDict() {
		clockwiseDict = new Dictionary<Direction, Direction>();

		clockwiseDict[Direction.Up] = Direction.Right;
		clockwiseDict[Direction.Right] = Direction.Down;
		clockwiseDict[Direction.Down] = Direction.Left;
		clockwiseDict[Direction.Left] = Direction.Up;
	}

	public void InitAntiClockwiseDict() {
		antiClockwiseDict = new Dictionary<Direction, Direction>();

		antiClockwiseDict[Direction.Up] = Direction.Left;
		antiClockwiseDict[Direction.Right] = Direction.Up;
		antiClockwiseDict[Direction.Down] = Direction.Right;
		antiClockwiseDict[Direction.Left] = Direction.Down;
	}

	public void InitBlockList() {
		blockList = new List<TileEnum> {
			// TileEnum.Ground,
			TileEnum.GroundBlock,
			TileEnum.GroundDiagonal,
			TileEnum.Algae
		};
	}

}
