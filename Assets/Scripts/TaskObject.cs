using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObject : MonoBehaviour {
	public DialogueManager dialogueManager;
	public TaskMenuManager taskMenuManager;

	public TextAsset taskObjectFile; //raw task object file used to describe taskobject
	public string[] dialogueStrs; //dialogue data structure 
	//public GameObject tipText;
	public bool tipActive;

	//used to access to the task menu info
	private Player player;

	void Start() {
		dialogueManager = FindObjectOfType<DialogueManager> ();
		player = FindObjectOfType<Player> ();
		//convert content of a dialgue file in to dialogues array
		if (taskObjectFile != null) {
			//dialogue strings is format of name : dialogue
			dialogueStrs = taskObjectFile.text.Split ('\n');
		}
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
				HandleTaskObject ();
			}
		}
	}

	//1) get task object 2)show get object message 3)update task panel
	void HandleTaskObject() {
		//1)
		gameObject.transform.parent = player.transform;//set object parent to the player
		dialogueManager.tipText.SetActive (false); //deactive tip menu
		gameObject.GetComponent<Collider2D>().enabled = false;//dis able task object collider
		//2)
		dialogueManager.dialogueStrs = this.dialogueStrs;
		dialogueManager.currentLine = 0;
		dialogueManager.ShowDialogue ();
		//3)
		string appendTask = dialogueStrs [0]; //get the first line of object description
		string newTask = this.taskMenuManager.taskText.text + "\n" + appendTask;
		this.taskMenuManager.updateTask (newTask);
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
