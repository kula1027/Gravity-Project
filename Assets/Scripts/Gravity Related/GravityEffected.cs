using UnityEngine;
using System.Collections;

public class GravityEffected : MonoBehaviour {
	protected float maxVelocity;
	protected GravityControl g_control;
	protected Rigidbody2D rb2d;
	protected Vector2 vel_saved;

	protected void GE_Start () {
		maxVelocity = 24f;
		g_control = GameObject.Find ("GravityControl").GetComponent<GravityControl> ();

		rb2d = GetComponent<Rigidbody2D> ();

		StartCoroutine ("GEbehavior");
	}

	private IEnumerator GEbehavior(){
		while (true) {
			if (g_control.g_onChange) {//not working currently except for stop
				vel_saved = rb2d.velocity;
				rb2d.velocity = Vector2.zero;
				StartCoroutine ("WaitForGC");
			}

			if (rb2d.velocity.magnitude >= maxVelocity) {//max velocity limitation
				rb2d.velocity = rb2d.velocity.normalized * maxVelocity;
			}
			yield return null;
		}
	}

	private IEnumerator WaitForGC(){
		while (true) {
			if (!g_control.g_onChange){
				rb2d.AddForce(vel_saved);
				yield break;
			}
			yield return null;
		}
	}
}
