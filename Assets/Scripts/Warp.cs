using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

	[SerializeField]
	private Transform target;
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			other.transform.position = target.transform.position;
		}
	}
}
