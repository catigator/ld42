using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameTime {
    public static bool isPaused = false;
    public static float deltaTime { get { return isPaused ? 0 : Time.deltaTime; } }
}

[System.Serializable]
public class Position {

	public int x;
	public int y;

	public Position(int x0, int y0) {
		x = x0;
		y = y0;
	}

	public void Add(Position position) {
		x += position.x;
		y += position.y;
	}

	public Position AddTemp(Position position) {
		return new Position(x + position.x, y + position.y);
	}

	public override string ToString() {
		return x.ToString() + "_" + y.ToString();
	}

	public string ScoreString() {
		return ToString() + "_" + "S";
	}

	public string HighScoreString() {
		return ToString() + "_" + "S";
	}

	public override int GetHashCode()
	{
		return Animator.StringToHash(ToString());
		// return x.GetHashCode() ^ y.GetHashCode();
	}

	public override bool Equals(object obj) 
    { 
		return Equals(obj as Position); 
    }

	public bool Equals(Position pos) {
		return (x == pos.x && y == pos.y); 
	}

}

[System.Serializable]
public class Level {
	public int number;
	public bool clearedLevel;
	public List<string> texts;

	public Level(int nbr) {
		number = nbr;
		texts = new List<string>();

	}

	public override string ToString() {
		return "Level " + number.ToString();
	}

}