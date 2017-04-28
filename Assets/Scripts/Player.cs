using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int moveSpeed;
	public int hp;
	public int currentHp;

	private Animator playerAnimator;
	private bool isDead;
	private float hDir; 
	private float vDir; 
	// Use this for initialization
	public static bool isTalking {
		get;
		set;
	}
	void Start () {
		isDead = false;
		playerAnimator = GetComponent<Animator> ();
		hp = 10;
		currentHp = hp;
	}

	void HandleDamage () {
		currentHp -= 1;
		if (currentHp <= 0) {
			playerAnimator.SetBool ("isDead", true);
			isDead = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Bee")) {
			print ("touch");
			HandleDamage ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		hDir = Input.GetAxisRaw ("Horizontal");
		vDir = Input.GetAxisRaw ("Vertical");
		HandleMovement ();	
		HandleAttack ();
	}

	//move vertically or horizontally based on player's axis raw
	void HandleMovement () {
		if (isTalking || isDead) { //can not move when talking
			playerAnimator.SetBool ("isTalking", true);
			playerAnimator.SetBool ("isWalking", false);
		} else {
			playerAnimator.SetBool ("isTalking", false);
			Vector2 moveDir = new Vector2 (hDir, vDir);
			if (moveDir != Vector2.zero) {
				playerAnimator.SetBool ("isWalking", true);
				playerAnimator.SetFloat ("hSpeed", moveDir.x);
				playerAnimator.SetFloat ("vSpeed", moveDir.y);
			} else {
				playerAnimator.SetBool ("isWalking", false);
			}
			//change position
			transform.Translate (new Vector3 ((moveDir.x * moveSpeed * Time.deltaTime)
			, (moveDir.y * moveSpeed * Time.deltaTime)
			, 0f));
		}
	}

	//player attack based on facing direction attack control: J
	void HandleAttack () {
		if (Input.GetKeyDown(KeyCode.J)) {
			playerAnimator.SetTrigger ("isAttacking");
		} 
	}
}
