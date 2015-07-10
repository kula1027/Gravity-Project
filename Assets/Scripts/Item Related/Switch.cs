using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	public GameObject switchOf;

	void OnTriggerStay2D(Collider2D coll){
		if (coll.gameObject.tag == "MovingObjects") {
			switchOf.SendMessage("SwitchOn");
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "MovingObjects") {
			switchOf.SendMessage("SwitchOff");
		}
	}
}
