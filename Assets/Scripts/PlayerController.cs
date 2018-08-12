using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D rb;
	public float turnSpeed;
	public float speed;
	public float maxSpeed;
	public Vector3 moveDirection;
	public float currentSpeed;

	public AudioManager am;
	public int maxHealth;
	public int health;

	public Text healthText;
	public SpriteRenderer subFlame;
	public CameraFollow cf;

	public BoardController bc;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		health = maxHealth;
		cf = GameObject.Find("LowResCamera").GetComponent<CameraFollow>();
		cf.FindPlayer();
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
		SetHealthText();

	}
	
	// Update is called once per frame
	void Update () {

		var turn = -Input.GetAxis("Horizontal");
		transform.Rotate(0, 0, turn * turnSpeed * Time.deltaTime);

		int verticalInput = (int) Mathf.Sign(Input.GetAxis("Vertical"));

		bool anyFlameInput = false;

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			verticalInput = 1;
			anyFlameInput = true;
		} else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			verticalInput = -1;
			anyFlameInput = true;
		} else {
			verticalInput = 0;
		}

		if (anyFlameInput) {
			subFlame.enabled = true;
		} else {
			subFlame.enabled = false;
		}

		currentSpeed = rb.velocity.magnitude;

		if (verticalInput != 0) {
			moveDirection = transform.right * verticalInput * speed;
			Vector3 currentVelocity = rb.velocity;
			currentVelocity += moveDirection * Time.deltaTime;
			if (!(Mathf.Abs(currentVelocity.magnitude) > maxSpeed)) {
				rb.velocity = currentVelocity;
			} else if (Mathf.Abs(currentVelocity.magnitude) < Mathf.Abs(currentSpeed)) {
				rb.velocity = currentVelocity;
			} 
		}
		
	}

	void SetHealthText() {
		bc.healthText.text = "HEALTH:\n" + health.ToString();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.layer != this.gameObject.layer) {
			am.playerHit.Play();
			health -= 1;
			SetHealthText();
			Debug.Log("COLLIDED WITH " + col.transform.name);

			if (health <= 0) {
				bc.mc.GoToGameOverScreen();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer != this.gameObject.layer) {
			am.playerHit.Play();
			health -= 1;
			SetHealthText();
			Debug.Log("TRIGGERED WITH " + col.transform.name);

			if (health <= 0) {
				bc.mc.GoToGameOverScreen();
			}
		}
	}

}
