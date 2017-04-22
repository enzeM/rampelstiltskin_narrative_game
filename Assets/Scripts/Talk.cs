using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Talk : MonoBehaviour {

	public Flowchart dialog;
	public string content;
	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (Input.GetKey (KeyCode.E)) {
				Block dialogBlock = dialog.FindBlock (content);
				dialog.ExecuteBlock (dialogBlock);
			}
		}
	}
}
