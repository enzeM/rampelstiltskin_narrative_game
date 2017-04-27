using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour {

	public DialogueManager dialogueManager;
	private Scene currentScene;

	// Use this for initialization
	void Start () {
		dialogueManager = FindObjectOfType<DialogueManager> ();	
	}
	
	void Awake () {
		currentScene = SceneManager.GetActiveScene ();
	}

	void Update() {
	}

	//need to have some state and decision
	public void ManageYesOption () {
		if (currentScene.name.Equals("Scene0")) {
			SceneManager.LoadScene ("Scene1");
		}
		dialogueManager.ResetDialogue ();
	}

	public void ManageNoOption() {
		if (currentScene.name.Equals ("Scene0")) {
			SceneManager.LoadScene ("Scene0");
		}
		dialogueManager.ResetDialogue ();
	}
}
