using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D rb;
	public float turnSpeed;
	public float speed;
	public float maxSpeed;
	public Vector3 moveDirection;
	public float currentSpeed;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		var turn = -Input.GetAxis("Horizontal");
		transform.Rotate(0, 0, turn * turnSpeed * Time.deltaTime);

		int verticalInput = (int) Mathf.Sign(Input.GetAxis("Vertical"));

		if (Input.GetKey(KeyCode.UpArrow)) {
			verticalInput = 1;
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			verticalInput = -1;
		} else {
			verticalInput = 0;
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

}
