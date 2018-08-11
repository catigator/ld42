using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {

	public float elapsedTime;
	public float bulletInterval;
	public GameObject bullet;
	public float velocity = 20f;
	public AudioManager am;

	// Use this for initialization
	void Start () {
		am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
	    if (elapsedTime > bulletInterval)
	    {
	        if (Input.GetKey(KeyCode.Space))
	        {
				am.playerShoot.Play();
	            GameObject newBullet = Instantiate(
					bullet, transform.position, transform.rotation) as GameObject;

				Vector3 newPosition = newBullet.transform.position;
				// TODO: need to make this rotation dependant...
				// newPosition.y -= 0.4f;
				newBullet.transform.position = newPosition;

	            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
	            rb.velocity = transform.right*velocity;

                elapsedTime = 0f;
	        }
	    }
	}
}
