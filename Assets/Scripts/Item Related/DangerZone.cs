using UnityEngine;
using System.Collections;

public class DangerZone : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.name == "Player") {
			coll.gameObject.GetComponent<CharacterControl>().Die();
		}
	}
}
