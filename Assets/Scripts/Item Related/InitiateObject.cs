using UnityEngine;
using System.Collections;

public class InitiateObject : MonoBehaviour {
	public GameObject obj;
	void OnTriggerEnter2D(Collider2D coll){
		obj.SetActive (true);
	}
}
