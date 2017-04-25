using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {

	public GameObject dialogueBox;
	public Text characterName;//character name
	public Text dialogue; //dialogue content 
	public string characterNameStr;
	public string[] dialogueStrs;//used to operate mutiple line
	public int currentLine; //current dialogue we are visiting
	public bool dialogueActive;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		HandleDialogue ();
	}

	public void ShowDialogue() {
		dialogueActive = true;
		dialogueBox.SetActive (true);
	}

	//set up dialogue screen when it is actived
	private void HandleDialogue() {
		if (dialogueActive) {
			Player.isTalking = true;
			if (Input.GetKeyDown (KeyCode.Space)) {
				this.currentLine++;
			}	
			//restart dialogue
			if (currentLine >= dialogueStrs.Length) {
				dialogueBox.SetActive (false);
				dialogueActive = false;
				this.currentLine = 0;
			}
			this.characterName.text = this.characterNameStr;
			this.dialogue.text = this.dialogueStrs [this.currentLine];
		} else {
			Player.isTalking = false;
		}
	}
}
