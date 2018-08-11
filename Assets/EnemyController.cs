using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject player;
	public Rigidbody2D rb;

	public Vector2 vectorToPlayer;

	public float speed;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		rb = this.GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		vectorToPlayer = player.transform.position - this.transform.position;

		rb.velocity = vectorToPlayer.normalized*speed;
	}
}
