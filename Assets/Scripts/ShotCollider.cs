using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCollider : MonoBehaviour {

	public AudioManager am;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.transform.tag != this.transform.tag) {
			am.explosion.Play();
			Debug.Log("TORPEDO TRIGGERED WITH " + col.transform.name);
			if (col.transform.tag != "Ground") {
				Destroy(col.transform.gameObject);
			}
			GameObject newExplosion = Instantiate(
					explosion, col.transform.position, transform.rotation) as GameObject;
			Destroy(this.gameObject);
		}
	}
}
