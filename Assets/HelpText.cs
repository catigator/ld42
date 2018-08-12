using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpText : MonoBehaviour {

	public int textInt;
	public string helpText;

	public TextMesh textMesh;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SetHelpText() {
		textMesh = this.GetComponent<TextMesh>();
		textMesh.text = helpText;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
