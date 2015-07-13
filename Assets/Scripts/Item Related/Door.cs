using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	void SwitchOn(){
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}

	void SwitchOff(){
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
	}
}
