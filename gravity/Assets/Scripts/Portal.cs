using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	public GameObject to;
	void OnTriggerEnter2D(Collider2D coll){
		coll.transform.position = to.transform.position;
	}
}
