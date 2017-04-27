using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {

	public GameObject tipText; //tip 
	public GameObject dialogueBox;
	public Text nameText;//character name
	public Text dialogueText; //dialogue content 
	public GameObject optionPanel;//yes or no option panel

	public string[] dialogueStrs; //strings in dialogueText
	private string nameStr; //string in nameText
	public int currentLine; //current dialogue we are visiting
	public bool dialogueActive; //dialogue box's condition

	public bool optionPanelActive; //option panel's contition

	//public Text taskText;//change task
	public TaskMenuManager taskMenuManager;
	private TaskObjectManager taskObjectManager;
	private bool haveTask;

	// Use this for initialization
	void Start () {
		dialogueActive = false;
		optionPanelActive = false;
		//init task object manager
		taskObjectManager = FindObjectOfType<TaskObjectManager> ();
		if (taskObjectManager == null) {
			haveTask = false;
		} else {
			haveTask = true;
		}
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
			if (Input.GetKeyDown (KeyCode.Space) && !optionPanelActive) {
				this.currentLine++;
			}	
			//restart dialogue
			if (currentLine >= dialogueStrs.Length) {
				ResetDialogue ();
			} else {
				string currentStr = this.dialogueStrs [this.currentLine];
				//deal with the conversation format name : dialogue
				if (currentStr.Contains (":")) { 
					this.nameText.text = currentStr.Split (':') [0];
					this.dialogueText.text = currentStr.Split (':') [1];
					//check task object and print relevant dialogue
					//format accept dialogue > no dialogue
				} else if (currentStr.Contains ("/")) {
					if (haveTask) {
						print ("have task");
						string[] strs = currentStr.Split ('/');
						string acceptStr = strs [0];
						string denialStr = strs [1];
						if (taskObjectManager.CheckTaskObject ()) {
							print ("accept");
							this.dialogueText.text = acceptStr;
						} else {
							this.dialogueText.text = denialStr;
							//jump to the last line, ensure last line is blank for task object case
							int lastLine = dialogueStrs.Length - 1;
							currentLine = lastLine;
						}
					} else {
						ResetDialogue (); //do nothing
					}
					//active the button panel
				} else if (currentStr.Equals ("[Option]")) { 
					optionPanelActive = true;
					optionPanel.SetActive (true);
					//update task format [task] $ task content
				} else if (currentStr.Contains ("$")) {
					print ("updated task");
					this.taskMenuManager.updateTask (currentStr.Split ('$') [1]);
				}
			}
		} else {
			Player.isTalking = false;
		}
	}

	public void ResetDialogue () {
		dialogueBox.SetActive (false);
		dialogueActive = false;
		optionPanelActive = false; //reset option
		optionPanel.SetActive (false);
		this.currentLine = 0;
	}
}
