using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	Transform playerTf;
	Vector3 targetPosition;
	
	void Start () {
		playerTf = GameObject.Find ("Player").transform;
	}

	private Vector3 velocity = Vector3.zero;
	void Update () {
		transform.rotation = playerTf.rotation;
		targetPosition = playerTf.transform.position + new Vector3(0, 0, -10);
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.8f);
	}
}
