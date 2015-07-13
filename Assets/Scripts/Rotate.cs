using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public const float rotSpeed = 40f;

	void Update () {
		transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotSpeed));
	}
}
