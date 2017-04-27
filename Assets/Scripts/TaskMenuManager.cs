using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskMenuManager: MonoBehaviour {
	public GameObject taskMenu;
	public Text taskText;
	private bool taskMenuActive = true;

	public void ManageTaskMenu () {
		if (taskMenuActive) {
			taskMenu.SetActive (true);
		} else {
			taskMenu.SetActive (false);
		}
		taskMenuActive = !taskMenuActive;
	}

	public void updateTask(string taskStr) {
		//print ("task updated");
		this.taskText.text = taskStr;
	}
}
