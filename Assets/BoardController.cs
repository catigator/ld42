using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {

	Up,
	Down,
	Left,
	Right,
	None

}; 

public class BoardController : MonoBehaviour {

	public Dictionary<int, TileEnum> tileDictionary;
	public Dictionary<TileEnum, GameObject> prefabDictionary;
	public Dictionary<int, Direction> directionDictionary; 

	public int gameSizeX;
	public int gameSizeY;

	public TileEnum[][] gameBoard;

	public GameObject playerPrefab;
	public GameObject groundPrefab;
	public GameObject algaePrefab;
	public GameObject evilFishprefab;
	public GameObject groundBlockPrefab;
	public GameObject groundDiagonalPrefab;


	// Use this for initialization
	void Start () {
		InitTileDictionary();
		InitDirectionDictionary();
		InitPrefabDictionary();

		gameBoard = InitGameBoard(gameBoard);
		LoadLevel("Map1_Ground", gameBoard);
		MakeTiles(gameBoard);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	TileEnum[][] InitGameBoard(TileEnum[][] board) {
		gameSizeX = 50;
		gameSizeY = 50;

		board = new TileEnum[gameSizeX][];

		for (int x = 0; x < gameSizeX; x++) {

			board [x] = new TileEnum[gameSizeY];

			for (int y = 0; y < gameSizeY; y++)
			{
				board [x][y] = TileEnum.None;
			}
		}

		return board;
	}

	public void InitTileDictionary() {
		tileDictionary = new Dictionary<int, TileEnum> ();

		tileDictionary [-1] = TileEnum.None;

		tileDictionary [0] = TileEnum.Ground;
		tileDictionary [1] = TileEnum.Ground;
		tileDictionary [2] = TileEnum.Ground;
		tileDictionary [3] = TileEnum.Ground;

		tileDictionary [4] = TileEnum.GroundBlock;

		tileDictionary [6] = TileEnum.Player;
		tileDictionary [7] = TileEnum.EvilFish;

		tileDictionary [8] = TileEnum.GroundDiagonal;
		tileDictionary [9] = TileEnum.GroundDiagonal;
		tileDictionary [10] = TileEnum.GroundDiagonal;
		tileDictionary [11] = TileEnum.GroundDiagonal;

		tileDictionary [12] = TileEnum.Algae;
		tileDictionary [13] = TileEnum.Algae;
		tileDictionary [14] = TileEnum.Algae;
		tileDictionary [15] = TileEnum.Algae;

	}

	public void InitPrefabDictionary() {
		prefabDictionary = new Dictionary<TileEnum, GameObject> ();

		// prefabDictionary[TileEnum.Player] = playerPrefab;
		prefabDictionary[TileEnum.EvilFish] = evilFishprefab;
		prefabDictionary[TileEnum.Ground] = groundPrefab;
		prefabDictionary[TileEnum.GroundBlock] = groundBlockPrefab;
		prefabDictionary[TileEnum.GroundDiagonal] = groundDiagonalPrefab;
		prefabDictionary[TileEnum.Algae] = algaePrefab;

	}

	public void InitDirectionDictionary() {
		directionDictionary = new Dictionary<int, Direction> ();

		directionDictionary [0] = Direction.Down;
		directionDictionary [1] = Direction.Left;
		directionDictionary [2] = Direction.Up;
		directionDictionary [3] = Direction.Right;

		directionDictionary [4] = Direction.Down;

		directionDictionary [6] = Direction.Down;
		directionDictionary [7] = Direction.Down;

		directionDictionary [8] = Direction.Down;
		directionDictionary [9] = Direction.Left;
		directionDictionary [10] = Direction.Up;
		directionDictionary [11] = Direction.Right;

		directionDictionary [12] = Direction.Down;
		directionDictionary [13] = Direction.Left;
		directionDictionary [14] = Direction.Up;
		directionDictionary [15] = Direction.Right;

	}

	
	public void LoadLevel(string level, TileEnum[][] board) {
		Debug.Log(board);

		if (tileDictionary == null) {
			InitTileDictionary ();
		}

		Debug.Log ("Loading level " + level);

		TextAsset txt = (TextAsset)Resources.Load(level, typeof(TextAsset));
		string filecontent = txt.text;
		string[] lines = filecontent.Split("\n"[0]);
		Debug.Log ("Loading Level");

		int size = lines.Length - 1;

		for (int i_line = 0; i_line < lines.Length; i_line++)
		{
			int y = size - i_line - 1; // Yeeeah this is weird

			string[] lineData = (lines[i_line].Trim()).Split(","[0]);

			for (int i_col = 0; i_col < lineData.Length; i_col++)
			{
				if (lineData [i_col] != null && lineData [i_col] != "") {
					int tileInt;
					if (System.Int32.TryParse (lineData [i_col], out tileInt)) {
						
						var tileEnum = tileDictionary [tileInt];

						int x = i_col;
						board[x][y] = tileEnum;
						Debug.Log(x.ToString() + ", " + y.ToString() + ": " + tileEnum.ToString());
					}
				}

			}

		}			
	}

	void MakeTiles(TileEnum[][] board) {
		Debug.Log("MakeTiles");
		for (int x = 0; x < gameSizeX; x++) {

			for (int y = 0; y < gameSizeY; y++) {
				
				if (board[x][y] != TileEnum.None) {

					if (prefabDictionary.ContainsKey(board[x][y])) {
						GameObject prefab = prefabDictionary[board[x][y]];

						GameObject obj = MakeTile(x, y, prefab);
						// Transform tile = obj.transform.GetChild(0);
						// SetFloorRotation(x, y, tile, board);
					}

				}
			}
		}
	}

	GameObject MakeTile(float x, float y, GameObject prefab) {
		Debug.Log("MakeTile: " + prefab.transform.name);
		GameObject obj = (GameObject)Instantiate (prefab);
		Vector3 position = new Vector3(x, y, 0);
		obj.transform.position = position;
		obj.transform.parent = this.transform;
		return obj;
	}

		
}
