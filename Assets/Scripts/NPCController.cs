using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
	public float walkSpeed;
	public float waitTime;
	private float waitTimeCounter;

	public float walkTime;
	private float walkTimeCounter;
	public bool isWalking;

	private Rigidbody2D npcBody;
	private Animator npcAnimator;
	private int walkDirection;

	public bool moveVertical;
	public bool moveHorizontal;

	public int patrolState;

	void Start () {
		//init patrol condition
		if (moveVertical) {
			patrolState = Random.Range (0, 2);
		} else if (moveHorizontal) {
			patrolState = Random.Range (2, 4);
		} 

		npcBody = GetComponent<Rigidbody2D> ();
		npcAnimator = GetComponent<Animator> ();

		//more random counter
		waitTimeCounter = Random.Range (waitTime * 0.7f, waitTime * 1.4f);
		walkTimeCounter = Random.Range(walkTime * 0.7f, walkTime * 1.4f);
		ChooseDirection ();
	}

	void Update () {
		HandleNPCMovement ();		
	}

	public void ChooseDirection () {
		if (moveHorizontal) {
			//walkDirection = Random.Range (2, 4);
			Patrol();
		} else if (moveVertical) {
			Patrol();
			//walkDirection = Random.Range (0, 2);
		} else if (moveHorizontal && moveVertical) {
			walkDirection = Random.Range (0, 4);
		} else {
			return;
		}
		isWalking = true;
		npcAnimator.SetBool ("isWalking", true);
		walkTimeCounter = walkTime;
	}

	private void Patrol () {
		if (moveHorizontal) {
			if (patrolState == 3) {
				walkDirection = 2;
			} else if (patrolState == 2) {
				walkDirection = 3;
			}
		} else if (moveVertical) {
			if (patrolState == 0) {
				walkDirection = 1;
			} else if (patrolState == 1) {
				walkDirection = 0;
			}
		}	
		patrolState = walkDirection;
	}

	void OnCollisionEnter2D (Collision2D other) {
		isWalking = false;
		npcAnimator.SetBool ("isWalking", false);
		waitTimeCounter = waitTime;
	}

	//if player in the talk zone npc stop moving
	void OnTriggerStay2D (Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			isWalking = false;
			npcAnimator.SetBool ("isWalking", false);
			npcAnimator.SetFloat ("vSpeed", 0);
			npcAnimator.SetFloat ("hSpeed", 0);
			waitTimeCounter = waitTime;
		}
	}

	//if player exit talk zone npc can move
	void OnTriggerExit2D (Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			isWalking = true;
			npcAnimator.SetBool ("isWalking", true);
			walkTimeCounter = walkTime;
		}
	}

	private void HandleNPCMovement() {
		if (isWalking) {
			walkTimeCounter -= Time.deltaTime;
			switch (walkDirection) {
			case 0: //up
				npcBody.velocity = new Vector2 (0, walkSpeed);
				npcAnimator.SetFloat ("vSpeed", walkSpeed);
				npcAnimator.SetFloat ("vWait", walkSpeed);
				break;
			case 1://down
				npcBody.velocity = new Vector2 (0, -walkSpeed);
				npcAnimator.SetFloat ("vSpeed", -walkSpeed);
				npcAnimator.SetFloat ("vWait", -walkSpeed);
				break;
			case 2: //right
				npcBody.velocity = new Vector2 (walkSpeed, 0);
				npcAnimator.SetFloat ("hSpeed", walkSpeed);
				npcAnimator.SetFloat ("hWait", walkSpeed);
				break;
			case 3://left
				npcBody.velocity = new Vector2 (-walkSpeed, 0);
				npcAnimator.SetFloat ("hSpeed", -walkSpeed);
				npcAnimator.SetFloat ("hWait", -walkSpeed);
				break;
			}
			if (walkTimeCounter < 0f) { //change state to wait
				isWalking = false;
				npcAnimator.SetBool ("isWalking", false);
				waitTimeCounter = waitTime;
			}
		} else {
			waitTimeCounter -= Time.deltaTime;

			npcBody.velocity = Vector2.zero;
			npcAnimator.SetFloat ("vSpeed", 0);
			npcAnimator.SetFloat ("hSpeed", 0);
			if (waitTimeCounter < 0f) { //change state to walk
				ChooseDirection ();
			}
		}
	}
}