using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour {

	public Player player;
	public Slider hpBar;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();	
	}
	
	// Update is called once per frame
	void Update () {
		hpBar.maxValue = player.hp;
		hpBar.value = player.currentHp;
	}
}
