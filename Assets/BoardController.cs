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
	public Dictionary<int, Level> levels;
	public List<Level> levelsList;

	public TileEnum[][] gameBoard;
	public TileEnum[][] objectBoard;
	public Direction[][] directionBoard;
	public Direction[][] directionGroundBoard;
	public int[][] textIntBoard;
	

	public GameObject playerPrefab;
	public GameObject groundPrefab;
	public GameObject algaePrefab;
	public GameObject blueAlgaePrefab;
	public GameObject evilFishprefab;
	public GameObject groundBlockPrefab;
	public GameObject groundDiagonalPrefab;
	public GameObject goalPrefab;
	public GameObject textPrefab;
	public GameObject greenKillaPrefab;

	public AudioManager am;

	public Text healthText;
	public Text levelText;
	public GameObject player;
	public GameObject tilesObject;
	public int maxLevel;
	public int clearedLevel;

	public MenuController mc;
	public Util util;

	// Use this for initialization
	void Start () {

		gameSizeX = 200;
		gameSizeY = 200;

		util = this.GetComponent<Util>();
		util.InitDicts();
		mc = GameObject.Find("MenuController").GetComponent<MenuController>();
		am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		currentLevel = 0;
		InitTileDictionary();
		InitDirectionDictionary();
		InitDegreesDict();
		InitPrefabDictionary();
		InitLevels();

		
	}

	public void BeginLevel(int level) {

		currentLevel = level;

		directionBoard = InitDirectionBoard(directionBoard);
		directionGroundBoard = InitDirectionBoard(directionGroundBoard);

		gameBoard = InitGameBoard(gameBoard);
		objectBoard = InitGameBoard(objectBoard);
		textIntBoard = InitTextIntBoard(textIntBoard);

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

		LoadLevelInt("Map" + levelBase +"_Text", textIntBoard);
		HandleTextIntBoard();

		SetLevelText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitLevels() {
		levels = new Dictionary<int, Level>();
		levelsList = new List<Level>();
		for (int i = 1; i <= maxLevel; i++)
		{
			levels[i] = new Level(i);
			levelsList.Add(levels[i]);
		}

		levels[1].texts.Add("MOVE WITH\nDIRECTION KEYS\nOR WASD");
		levels[1].texts.Add("THRUST WITH\nUP/DOWN OR W/S");
		levels[1].texts.Add("ROTATE WITH\nLEFT/RIGHT OR A/D");
		levels[1].texts.Add("SHOOT WITH\n SPACE");
		levels[1].texts.Add("ENTER PORTAL TO\nWARP!!\nTO NEXT ZONE");

		levels[2].texts.Add("RED ALGEA\nGROWS\n& GROWS\n& GROWS");

		levels[3].texts.Add("FISH\nARE\nEVIL");

		levels[4].texts.Add("BEWARE THE\nGREEN KILLAH");
	}

	void HandleTextIntBoard() {
		for (int x = 0; x < textIntBoard.Length; x++)
		{
			for (int y = 0; y < textIntBoard.Length; y++)
			{
				if (textIntBoard[x][y] != -1) {
					GameObject obj = MakeTile(x, y, textPrefab);

					HelpText ht = obj.GetComponent<HelpText>();
					int textInt = textIntBoard[x][y];
					ht.textInt = textInt;
					Debug.Log("currentLeveL: " + currentLevel.ToString());
					Debug.Log("level: " + levels[currentLevel].ToString());
					Debug.Log("textInt: " + textInt.ToString());

					ht.helpText = levels[currentLevel].texts[textInt];
					ht.SetHelpText();
				}
			}
		}
	}

	int[][] InitTextIntBoard(int[][] board) {

		board = new int[gameSizeX][];

		for (int x = 0; x < gameSizeX; x++) {

			board [x] = new int[gameSizeY];

			for (int y = 0; y < gameSizeY; y++)
			{
				board [x][y] = -1;
			}
		}

		return board;

	}

	TileEnum[][] InitGameBoard(TileEnum[][] board) {

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

		tileDictionary [17] = TileEnum.BlueAlgae;
		tileDictionary [18] = TileEnum.BlueAlgae;
		tileDictionary [19] = TileEnum.BlueAlgae;
		tileDictionary [20] = TileEnum.BlueAlgae;

		tileDictionary [21] = TileEnum.GreenKilla;


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
		prefabDictionary[TileEnum.BlueAlgae] = blueAlgaePrefab;
		prefabDictionary[TileEnum.GreenKilla] = greenKillaPrefab;

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

		directionDictionary [17] = Direction.Down;
		directionDictionary [18] = Direction.Left;
		directionDictionary [19] = Direction.Up;
		directionDictionary [20] = Direction.Right;

		directionDictionary [21] = Direction.None;

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

	public void LoadLevelInt(string level, int[][] board) {
		Debug.Log(board);

		Debug.Log ("Loading level int " + level);

		TextAsset txt = (TextAsset)Resources.Load(level, typeof(TextAsset));
		string filecontent = txt.text;
		string[] lines = filecontent.Split("\n"[0]);
		Debug.Log ("Loading LevelInt");

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
						int x = i_col;
						board[x][y] = tileInt;
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
		AnimatorClipInfo aci = clipInfo[0];

		currAnimName = aci.clip.name;
        return currAnimName;
 
     }

	 void SetLevelText() {
		levelText.text = currentLevel.ToString();
	}

		
}
