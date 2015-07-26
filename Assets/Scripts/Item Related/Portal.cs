using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	public GameObject to;
	private GameObject sentObj;
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.GetComponent<IngameObject> ().enterPortal) {
			coll.transform.position = to.transform.position;
			coll.gameObject.GetComponent<IngameObject> ().enterPortal = false;
		}
	}
	void OnTriggerExit2D(Collider2D coll){
		coll.gameObject.GetComponent<IngameObject> ().enterPortal = true;
		Debug.Log ("EXIT");
	}
}
