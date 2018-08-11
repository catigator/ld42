using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

	public float elapsedTime;
	public float destroyAfterTime;

	// Use this for initialization
	void Start () {
		elapsedTime = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;

		if (elapsedTime > destroyAfterTime) {
			Destroy(this.gameObject);
		}
		
	}
}
