using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{

	    // Vector3 newPos = new Vector3(0,0,0);
	    // newPos.z = player.transform.position.z - 6f;
	    // newPos.y = player.transform.position.y + 3f;
	    // newPos.x = 1.0f*player.transform.position.x;

		Vector3 newPos = this.transform.position;
		newPos.x = player.transform.position.x;
		newPos.y = player.transform.position.y;

	    this.transform.position = newPos;

	}
}
