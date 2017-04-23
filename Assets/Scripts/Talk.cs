using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Talk : MonoBehaviour {

	public static Flowchart dialogsManager;
	public Flowchart dialogs;
	public string content;

	void Awake() {
		dialogsManager = GameObject.Find ("DialogsManager").GetComponent<Flowchart> ();
	}
	public static bool isTalking {
		get {
			return dialogsManager.GetBooleanVariable ("isTalking");
		}
	}


	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (Input.GetKey (KeyCode.E)) {
				Block dialogsBlock = dialogs.FindBlock (content);
				dialogs.ExecuteBlock (dialogsBlock);
			}
		}
	}
}
