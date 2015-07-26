using UnityEngine;
using System.Collections;

public class IngameObject : MonoBehaviour {
	public bool enterPortal;
	protected GravityControl g_control;
	protected Rigidbody2D rb2d;

	protected void Obj_Start () {
		enterPortal = true;
		g_control = GameObject.Find ("GravityControl").GetComponent<GravityControl> ();

		rb2d = GetComponent<Rigidbody2D> ();

		StartCoroutine ("GEbehavior");
	}

	private IEnumerator GEbehavior(){
		while (true) {
			if (g_control.G_onChange) {
				rb2d.velocity = Vector2.zero;
			}

			if (rb2d.velocity.magnitude >= CONFIG.maxVelocity) {//limit max velocity
				rb2d.velocity = rb2d.velocity.normalized * CONFIG.maxVelocity;
			}
			yield return null;
		}
	}
}
