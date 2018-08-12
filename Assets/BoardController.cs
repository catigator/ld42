using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

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
	public Dictionary<Direction, float> degreesDict;


	public int gameSizeX;
	public int gameSizeY;

	public int currentLevel;

	public TileEnum[][] gameBoard;
	public TileEnum[][] objectBoard;
	public Direction[][] directionBoard;
	public Direction[][] directionGroundBoard;
	

	public GameObject playerPrefab;
	public GameObject groundPrefab;
	public GameObject algaePrefab;
	public GameObject evilFishprefab;
	public GameObject groundBlockPrefab;
	public GameObject groundDiagonalPrefab;
	public GameObject goalPrefab;

	public Text healthText;
	public Text levelText;
	public GameObject player;
	public GameObject tilesObject;
	public int maxLevel;

	public MenuController mc;
	public Util util;

	// Use this for initialization
	void Start () {
		util = this.GetComponent<Util>();
		util.InitDicts();
		mc = GameObject.Find("MenuController").GetComponent<MenuController>();
		currentLevel = 0;
		InitTileDictionary();
		InitDirectionDictionary();
		InitDegreesDict();
		InitPrefabDictionary();
		maxLevel = 3;
		
	}

	public void BeginLevel(int level) {

		currentLevel = level;

		directionBoard = InitDirectionBoard(directionBoard);
		directionGroundBoard = InitDirectionBoard(directionGroundBoard);

		gameBoard = InitGameBoard(gameBoard);
		objectBoard = InitGameBoard(objectBoard);

		string levelBase = level.ToString();
		if (level > maxLevel) {
			levelBase = "1";
			currentLevel = 1;
		}

		LoadLevel("Map" + levelBase +"_Ground", gameBoard);
		MakeTiles(gameBoard);
		LoadLevel("Map" + levelBase +"_Algae", objectBoard);
		LoadLevel("Map" + levelBase +"_Enemies", objectBoard);
		LoadLevel("Map" + levelBase +"_Objects", objectBoard);
		MakeTiles(objectBoard);
		SetLevelText();
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

	Direction[][] InitDirectionBoard(Direction[][] board) {
		gameSizeX = 50;
		gameSizeY = 50;

		board = new Direction[gameSizeX][];

		for (int x = 0; x < gameSizeX; x++) {

			board [x] = new Direction[gameSizeY];

			for (int y = 0; y < gameSizeY; y++)
			{
				board [x][y] = Direction.None;
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

		tileDictionary [16] = TileEnum.Goal;

	}

	public void InitDegreesDict() {
		degreesDict = new Dictionary<Direction, float> ();

		degreesDict[Direction.Down] = 0f;
		degreesDict[Direction.Left] = 270f;
		degreesDict[Direction.Up] = 180f;
		degreesDict[Direction.Right] = 90f;

		degreesDict[Direction.None] = 0f;

	}

	public void InitPrefabDictionary() {
		prefabDictionary = new Dictionary<TileEnum, GameObject> ();

		prefabDictionary[TileEnum.Player] = playerPrefab;
		prefabDictionary[TileEnum.EvilFish] = evilFishprefab;
		prefabDictionary[TileEnum.Ground] = groundPrefab;
		prefabDictionary[TileEnum.GroundBlock] = groundBlockPrefab;
		prefabDictionary[TileEnum.GroundDiagonal] = groundDiagonalPrefab;
		prefabDictionary[TileEnum.Algae] = algaePrefab;
		prefabDictionary[TileEnum.Goal] = goalPrefab;

	}

	public void InitDirectionDictionary() {
		directionDictionary = new Dictionary<int, Direction> ();

		directionDictionary [-1] = Direction.None;

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

		directionDictionary [16] = Direction.None;

	}

	public void HandleAlgaeTile(GameObject obj, int x, int y, Direction dir) {
		AlgaeController ac = obj.GetComponent<AlgaeController>();
		ac.position = new Position(x, y);
		ac.direction = dir;

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

						if (tileEnum != TileEnum.None) {

							int x = i_col;
							board[x][y] = tileEnum;

							if (tileEnum == TileEnum.Ground) {
								var direction = directionDictionary[tileInt];
								directionGroundBoard[x][y] = direction;
							} else {
								var direction = directionDictionary[tileInt];
								directionBoard[x][y] = direction;
							}

						}
					}
				}

			}

		}			
	}

	void MakeTiles(TileEnum[][] board) {
		for (int x = 0; x < gameSizeX; x++) {

			for (int y = 0; y < gameSizeY; y++) {
				
				if (board[x][y] != TileEnum.None) {

					if (prefabDictionary.ContainsKey(board[x][y])) {
						GameObject prefab = prefabDictionary[board[x][y]];

						GameObject obj = MakeTile(x, y, prefab);

						if (board[x][y] == TileEnum.Ground) {
							float rotation = degreesDict[directionGroundBoard[x][y]];
							obj.transform.eulerAngles = new Vector3(0,0, rotation);
						} else {
							float rotation = degreesDict[directionBoard[x][y]];
							obj.transform.eulerAngles = new Vector3(0,0, rotation);
						}

						if (obj.transform.tag == "Ground") {
							SetAnimationFrame(obj);
						}

						if (obj.transform.tag == "Player") {
							player = obj;
						}

						if (board[x][y] == TileEnum.Algae) {
							Direction oppositeDir = util.oppositeDirectionDict[directionBoard[x][y]];
							HandleAlgaeTile(obj, x, y, oppositeDir);
						}

					} 
					// else if (board[x][y] == TileEnum.Player) {
					// 	GameObject player = GameObject.Find("Player");
					// 	player.transform.position = new Vector3(x,y,0);
					// 	Debug.Log("Player at " + x.ToString() + " , " + y.ToString());
					// }

				}
			}
		}
	}

	public GameObject MakeTile(float x, float y, GameObject prefab) {
		GameObject obj = (GameObject)Instantiate (prefab);
		Vector3 position = new Vector3(x, y, 0);
		obj.transform.position = position;
		obj.transform.parent = tilesObject.transform;
		
		return obj;
	}

	void SetAnimationFrame(GameObject obj) {
		var anim = obj.GetComponent<Animator>();
		int frame = Random.Range(0,4);
		string currAnimName = GetCurrentAnimationName(anim);
        anim.Play(currAnimName, 0, ( 1f / 4f ) * frame);
		anim.speed = 0f;

	}

	string GetCurrentAnimationName(Animator anim)
     {
        var currAnimName = "";
        var clipInfo = anim.GetCurrentAnimatorClipInfo(0);
		AnimatorClipInfo aci = clipInfo[0]; // I haven't seen clipInfo be larger than one before

		currAnimName = aci.clip.name;
        return currAnimName;
 
     }

	 void SetLevelText() {
		levelText.text = "ZONE:\n" + currentLevel.ToString();
	}

		
}
