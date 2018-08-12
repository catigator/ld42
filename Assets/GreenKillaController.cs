using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKillaController : MonoBehaviour {

	public BoardController bc;

	public Vector2 vectorToPlayer;
	public float elapsedTime;
	

	public float shotSpeed;
	public float bulletInterval;

	public GameObject bullet;

	// Use this for initialization
	void Start () {
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
		elapsedTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		vectorToPlayer = bc.player.transform.position - this.transform.position;
		vectorToPlayer = vectorToPlayer.normalized;

		elapsedTime += Time.deltaTime;
	    if (elapsedTime > bulletInterval)
	    {
			Shoot();
	    }
	}

	void Shoot() {
		// playerShoot.Play();
		GameObject newBullet = Instantiate(
			bullet, transform.position, transform.rotation) as GameObject;

		Vector3 newPosition = newBullet.transform.position;
		// TODO: need to make this rotation dependant...
		// newPosition.y -= 0.4f;
		newBullet.transform.position = newPosition;
		newBullet.transform.parent = this.transform.parent;

		Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
		rb.velocity = vectorToPlayer*shotSpeed;
		elapsedTime = 0f;

	}
}
