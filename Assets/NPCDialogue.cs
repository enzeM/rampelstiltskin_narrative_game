using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour {

	//set character name in dialogue box
	public string characterNameStr;

	//the dialogue content
	public TextAsset dialoguesFile;
	public string[] dialogueStrs;
	public string dialogue;
	public DialogueManager dialogueManager;

	public GameObject tipText;
	public bool tipActive;

	// Use this for initialization
	void Start () {
		dialogueManager = FindObjectOfType<DialogueManager> ();	
		//convert content of a dialgue file in to dialogues array
		if (dialoguesFile != null) {
			dialogueStrs = dialoguesFile.text.Split ('\n');
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
			if (Input.GetKey (KeyCode.E) && !dialogueManager.dialogueActive) {
				dialogueManager.characterNameStr = this.characterNameStr;
				dialogueManager.dialogueStrs = this.dialogueStrs;
				dialogueManager.currentLine = 0;
				dialogueManager.ShowDialogue ();
			}
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			tipText.SetActive (false);
		}
	}

	//display tip if a special object is able to be trigger
	void ManageTip(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (!tipActive && !dialogueManager.dialogueActive) {
				tipText.SetActive (true);
			} else {
				tipText.SetActive (false);
			}
		}
	}
}
