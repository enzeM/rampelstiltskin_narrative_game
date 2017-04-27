using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskObjectManager : MonoBehaviour {

	public TaskObject[] taskObjects;

	private Player player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();	
	}

	//check task object list
	public bool CheckTaskObject () {
		for (int i = 0; i < this.taskObjects.Length; i++) {
			try {
				GameObject task;
				task = player.transform.Find(taskObjects[i].name).gameObject;
			}       
			catch (NullReferenceException ex) {
				return false;
			}
		}
		return true;
	}
}
