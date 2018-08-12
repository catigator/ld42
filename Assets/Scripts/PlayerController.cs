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
	public Vector2 reflVelocity;
	public float currentSpeed;

	public AudioManager am;
	public int maxHealth;
	public int health;

	public Text healthText;
	public SpriteRenderer subFlame;
	public CameraFollow cf;

	public BoardController bc;

	public float collisionSpeed;

	public GameObject explosion;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		health = maxHealth;
		cf = GameObject.Find("LowResCamera").GetComponent<CameraFollow>();
		cf.FindPlayer();
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
		SetHealthText();
		reflVelocity = new Vector2(0, 0);

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

		// Debug.DrawLine(transform.position, (Vector2)transform.position + rb.velocity, Color.red);
		// Debug.DrawLine(transform.position, (Vector2)transform.position + reflVelocity, Color.blue);
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

			HandleReflection(col);

			if (health <= 0) {
				bc.mc.GoToGameOverScreen();
			}
		}
	}

	// void OnTriggerEnter2D(Collider2D col) {
	// 	if (col.gameObject.layer != this.gameObject.layer) {
	// 		am.playerHit.Play();
	// 		health -= 1;
	// 		SetHealthText();
	// 		Debug.Log("TRIGGERED WITH " + col.transform.name);

	// 		if (health <= 0) {
	// 			bc.mc.GoToGameOverScreen();
	// 		}
	// 	}
	// }

	void HandleReflection(Collision2D col) {
		Vector2 inDirection = rb.velocity;
		if (col.contacts.Length > 0) {
			Vector2 inNormal = col.contacts[0].normal;
			reflVelocity = Vector2.Reflect(transform.right, inNormal);

			Vector2 newVelocity = reflVelocity*3f; // + collisionSpeed*rb.velocity;
			rb.velocity = newVelocity;
			MakeExplosion(col.contacts[0].point, transform.rotation, this.transform.parent);
		}
	}

	public void MakeExplosion(Vector3 pos, Quaternion rot, Transform parent) {
		GameObject newExplosion = Instantiate(
					explosion, pos, rot) as GameObject;
			newExplosion.transform.parent = parent;
	}

}
