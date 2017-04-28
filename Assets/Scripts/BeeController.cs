using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

	public int moveSpeed;
	public float timeBetweenMove;
	public float timeToMove;
	public int hp = 3;

	private Animator beeAnimator;
	private bool moving;
	private Vector3 moveDirection;
	private float timeBetweenMoveCounter;
	private float timeToMoveCounter;

	private float dietime = 2f;

	private bool isDead = false;
	// Use this for initialization
	void Start () {
		beeAnimator = GetComponent<Animator> ();
		timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.7f, timeBetweenMove * 1.4f);
		timeToMoveCounter = Random.Range(timeToMove * 0.7f, timeToMove * 1.4f);
	}
	
	// Update is called once per frame
	void Update () {
		FlyAround ();
		HandleDead ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Sword")) {
			HandleDamage ();
		}
	}

	void HandleDead () {
		if (isDead) {
			dietime -= Time.deltaTime;
		}
		if (dietime < 0f) {
			gameObject.SetActive (false);
		}
	}

	void HandleDamage() {
		hp -= 1;
		if (this.hp <= 0) {
			isDead = true;
			moveSpeed = 0;
			beeAnimator.SetBool ("isDead", true);
		}
	}

	//fly randomly in the map
	void FlyAround() {
		if (moving) {
			timeToMoveCounter -= Time.deltaTime;
			beeAnimator.SetBool ("isFlying", true);
			beeAnimator.SetFloat ("vSpeed", moveDirection.x);	
			beeAnimator.SetFloat ("hSpeed", moveDirection.y);	
			transform.Translate (new Vector3 (
				(moveDirection.x * moveSpeed * Time.deltaTime),
				(moveDirection.y * moveSpeed * Time.deltaTime),
				0f));
			if (timeToMoveCounter < 0f) {
				moving = false;
				beeAnimator.SetBool ("isFlying", false);
				timeBetweenMoveCounter -= timeBetweenMove;
			}
		} else {
			timeBetweenMoveCounter -= Time.deltaTime;
			if (timeBetweenMoveCounter < 0f) {
				moving = true;
				timeToMoveCounter = timeToMove;
				float rangeX = Random.Range (-1f, 1f);
				float rangeY = Random.Range (-1f, 1f) ;
				moveDirection = new Vector3 (rangeX, rangeY, 0f);
			}
		}
	}
}
