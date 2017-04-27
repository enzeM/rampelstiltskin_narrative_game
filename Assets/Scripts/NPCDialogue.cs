using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour {

	public TextAsset dialoguesFile; //raw dialogue file
	public string[] dialogueStrs; //dialogue data structure 
	public DialogueManager dialogueManager;

	//public GameObject tipText;
	public bool tipActive;
	
	// Use this for initialization
	void Start () {
		dialogueManager = FindObjectOfType<DialogueManager> ();	
		//convert content of a dialgue file in to dialogues array
		if (dialoguesFile != null) {
			//dialogue strings is format of name : dialogue
			dialogueStrs = dialoguesFile.text.Split ('\n');
		} else { //avoid null pointer exception
			dialogueStrs [0] = "There is nothing special here";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		ManageTip (other);	
	}

	void OnTriggerStay2D (Collider2D other) {
		ManageTip (other);
		if (other.gameObject.CompareTag ("Player")) {
			//active a dialogue (read from the beginning) when 
			//no other dialogue is active and the option panel is also deactive
			if (Input.GetKey (KeyCode.E) 
					&& !dialogueManager.dialogueActive 
					&& !dialogueManager.optionPanelActive) {
				dialogueManager.dialogueStrs = this.dialogueStrs;
				dialogueManager.currentLine = 0;
				dialogueManager.ShowDialogue ();
			}
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			dialogueManager.tipText.SetActive (false);
		}
	}

	//display tip if a special object is able to be trigger
	void ManageTip(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (!tipActive && !dialogueManager.dialogueActive) {
				dialogueManager.tipText.SetActive (true);
			} else {
				dialogueManager.tipText.SetActive (false);
			}
		}
	}
}
