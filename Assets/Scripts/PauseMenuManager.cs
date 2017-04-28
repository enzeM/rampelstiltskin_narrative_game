using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	public GameObject pauseMenu;
	private bool isPause;

	// Use this for initialization
	void Start () {
		isPause = false;			
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput ();
		ManagePause ();	
	}

	void HandleInput() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPause = !isPause;
		} 
	}

	void ManagePause () {
		if (isPause) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
		pauseMenu.SetActive (isPause);
	}

	public void ReturnMainMenu() {
		SceneManager.LoadScene (0);
	}

	public void RestartCurrentGame() {
		string currentSceneName = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene (currentSceneName);
	}

	public void Quit() {
		Application.Quit ();
	}
}
