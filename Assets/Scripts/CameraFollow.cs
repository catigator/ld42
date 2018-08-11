using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public BoardController bc;

	// Use this for initialization
	void Start () {

        FindPlayer();
	}

	public void FindPlayer() {
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (bc.player != null) {
			Vector3 newPos = this.transform.position;
			newPos.x = bc.player.transform.position.x;
			newPos.y = bc.player.transform.position.y;
			this.transform.position = newPos;
		}
	}
}
