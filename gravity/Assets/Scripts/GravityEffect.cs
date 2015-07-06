using UnityEngine;
using System.Collections;

public class GravityEffect : MonoBehaviour {
	float maxVelocity;
	GravityControl g_control;
	Rigidbody2D rgb2d;
	Vector2 vel_saved;

	void Start () {
		maxVelocity = 24f;
		g_control = GameObject.Find ("GravityControl").GetComponent<GravityControl> ();
		rgb2d = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (rgb2d.velocity.magnitude >= maxVelocity) {//max velocity limitation
			rgb2d.velocity = rgb2d.velocity.normalized * maxVelocity;
		}

		if (g_control.g_onChange) {
			vel_saved = rgb2d.velocity;
			rgb2d.velocity = Vector2.zero;
			StartCoroutine("WaitForGc");
		}
	}

	IEnumerator WaitForGc(){
		while (true) {
			if (!g_control.g_onChange){
				rgb2d.AddForce(vel_saved);
				yield break;
			}
			yield return null;
		}
	}
}
