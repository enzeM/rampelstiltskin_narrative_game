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

	/*
	 * scene0: yes to scene1 no to intro
	 * scene1: yes to scene2 no to scene3
	 * scene2: yes to scene4 no to intro
	 * scene3: yes to scene5 no to intro
	 * scene4: yes to scene6 no to intro
	 * scene5: yes to scene6 no to intro
	 */
	public void ManageYesOption () {
		if (currentScene.name.Equals ("Scene0")) {
			SceneManager.LoadScene ("Scene1");
		} else if (currentScene.name.Equals ("Scene1")) {
			SceneManager.LoadScene ("Scene2");
		} else if (currentScene.name.Equals ("Scene2")) {
			SceneManager.LoadScene ("Scene4");
		} else if (currentScene.name.Equals ("Scene3")) {
			SceneManager.LoadScene ("Scene5");
		} else if (currentScene.name.Equals ("Scene4")) {
			SceneManager.LoadScene ("Scene6");
		} else if (currentScene.name.Equals ("Scene5")) {
			SceneManager.LoadScene ("Scene6");
		} else if (currentScene.name.Equals ("Scene6")) {
			SceneManager.LoadScene ("Intro");
		}
		dialogueManager.ResetDialogue ();
	}

	public void ManageNoOption() {
		if (currentScene.name.Equals ("Scene0")) {
			SceneManager.LoadScene ("Intro");
		} else if (currentScene.name.Equals ("Scene1")) {
			SceneManager.LoadScene ("Scene3");
		} else if (currentScene.name.Equals ("Scene2")) {
			SceneManager.LoadScene ("Intro");
		} else if (currentScene.name.Equals ("Scene3")) {
			SceneManager.LoadScene ("Intro");
		} else if (currentScene.name.Equals ("Scene4")) {
			SceneManager.LoadScene ("Intro");
		} else if (currentScene.name.Equals ("Scene5")) {
			SceneManager.LoadScene ("Intro");
		} else if (currentScene.name.Equals ("Scene6")) {
			QuitGame ();
		}
		dialogueManager.ResetDialogue ();
	}

	public void StartGame() {
		SceneManager.LoadScene ("Scene0");
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
