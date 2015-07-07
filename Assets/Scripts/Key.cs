using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {
	public GameObject switchOf;
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			switchOf.SendMessage("SwitchOn");
			gameObject.SetActive(false);
		}
	}
}
