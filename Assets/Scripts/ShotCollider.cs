using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotEnum {
	Player,
	Enemy
}

public class ShotCollider : MonoBehaviour {

	public AudioManager am;
	public GameObject explosion;
	public BoardController bc;
	public ShotEnum shotEnum;

	// Use this for initialization
	void Start () {
		am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		bc = GameObject.Find("BoardController").GetComponent<BoardController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.layer != this.gameObject.layer) {
			if (shotEnum == ShotEnum.Player) {
				HandlePlayerShot(col);
			} else if (shotEnum == ShotEnum.Enemy) {
				HandleEnemyShot(col);
			}
		}
	}

	public void HandlePlayerShot(Collider2D col) {
		if (col.transform.tag == "Algae") {
			AlgaeController ac = col.gameObject.GetComponent<AlgaeController>();
			bc.objectBoard[ac.position.x][ac.position.y] = TileEnum.None;
		}

		am.explosion.Play();
		Debug.Log("TORPEDO TRIGGERED WITH " + col.transform.name);
		if (col.transform.tag != "Ground" && col.transform.tag != "Goal" 
		&& col.transform.tag != "Player") {
			Destroy(col.transform.gameObject);
		}
		MakeExplosion(col.transform.position, transform.rotation, this.transform.parent);
		Destroy(this.gameObject);
	}

	public void HandleEnemyShot(Collider2D col) {
		if (col.transform.tag == "Player") {
			PlayerController pc = col.gameObject.GetComponent<PlayerController>();
			pc.ChangeHealth(-1);
			pc.HandleOtherReflection(this.GetComponent<Rigidbody2D>());
		}

		if (this.GetComponent<Renderer>().isVisible) {
			am.explosion.Play();
		}
		MakeExplosion(col.transform.position, transform.rotation, this.transform.parent);
		Destroy(this.gameObject);
		Debug.Log("Should have destroyed " + this.transform.name);
	}

	public void MakeExplosion(Vector3 pos, Quaternion rot, Transform parent) {
		GameObject newExplosion = Instantiate(
					explosion, pos, rot) as GameObject;
		newExplosion.transform.parent = parent;
	}

}
