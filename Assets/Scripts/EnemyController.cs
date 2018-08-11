using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject player;
	public Rigidbody2D rb;
	public BoardController bc;

	public Vector2 vectorToPlayer;

	public float speed;

	// Use this for initialization
	void Start () {
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
		rb = this.GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		vectorToPlayer = bc.player.transform.position - this.transform.position;
  		transform.rotation = Quaternion.FromToRotation(Vector3.left, vectorToPlayer);

		rb.velocity = vectorToPlayer.normalized*speed;
	}
}
