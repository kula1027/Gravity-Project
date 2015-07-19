using UnityEngine;
using System.Collections;

public class GravityControlPanel : MonoBehaviour {
	private bool activated;
	private GameObject player;
	private GameObject activeEffect;
	private GravityControl g_control;

	void Start(){
		activated = false;
		player = GameObject.Find ("Player");
		activeEffect = transform.FindChild("EnergyBall").gameObject;
		activeEffect.SetActive (false);

		g_control = GameObject.Find ("GravityControl").GetComponent<GravityControl> ();
	}

	Vector2 g_direction;
	int g_mode = 0;
	private void OnTriggerStay2D(Collider2D coll){
		activated = true;
		if (!g_control.G_onChange) {
			float input_v = Input.GetAxisRaw ("Vertical");
			float input_h = Input.GetAxisRaw ("Horizontal");


			if(input_h == 1 && Input.GetKey (KeyCode.LeftShift)){
				g_direction = HELPER.getDir_Right(Physics2D.gravity).normalized;
				g_mode = GetGmode (g_direction);
				if(g_mode != g_control.G_currentMode){
					g_control.G_onChange = true;
					player.GetComponent<CharacterControl>().StartRotate(true, g_mode);
				}
			}
			else if(input_v == 1 && Input.GetKey (KeyCode.LeftShift)){
				g_direction = -Physics2D.gravity.normalized;
				g_mode = GetGmode (g_direction);
				if(g_mode != g_control.G_currentMode){
					g_control.G_onChange = true;
					player.GetComponent<CharacterControl>().StartRotate(false, g_mode);
				}
			}
			else if(input_h == -1 && Input.GetKey (KeyCode.LeftShift)){
				g_direction = -HELPER.getDir_Right(Physics2D.gravity).normalized;
				g_mode = GetGmode (g_direction);
				if(g_mode != g_control.G_currentMode){
					g_control.G_onChange = true;
					player.GetComponent<CharacterControl>().StartRotate(false, g_mode);
				}
			}
		}
	}

	private int GetGmode(Vector2 g_direction){
		if(g_direction.Equals(-Vector2.up))
			return 0;
		if(g_direction.Equals(Vector2.right))
			return 1;
		if(g_direction.Equals(Vector2.up))
			return 2;
		if(g_direction.Equals(-Vector2.right))
			return 3;
		
		return -1;
	}
	
	#region Trigger
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.Equals(player)) {
			activated = true;
			activeEffect.SetActive(true);
			Debug.Log (gameObject.name + " Activated");

			StartCoroutine("EffectOn");
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.Equals(player)) {
			activated = false;
			Debug.Log (gameObject.name + " Deactivated");

			StartCoroutine("EffectOff");
		}
	}
	#endregion
	
	#region Effect
	bool beforeState;
	IEnumerator EffectOn(){
		beforeState = activated;
		SpriteRenderer EffectSR = activeEffect.GetComponent<SpriteRenderer> ();
		while (EffectSR.color.a <= 1) {
			float ta = EffectSR.color.a + 0.01f;
			EffectSR.color = new Color(1f, 1f, 1f, ta);
			Debug.Log (EffectSR.color.a);
			if(!activated){
				Debug.Log ("effect on");
				yield break;
			}
			yield return null;
		}
	}
	
	IEnumerator EffectOff(){
		SpriteRenderer EffectSR = activeEffect.GetComponent<SpriteRenderer> ();
		while (EffectSR.color.a >= 0) {
			float ta = EffectSR.color.a - 0.01f;
			EffectSR.color = new Color(1f, 1f, 1f, ta);
			if(activated){
				Debug.Log ("effect off");
				yield break;
			}
			yield return null;
		}
		activeEffect.SetActive (false);
		Debug.Log ("gone");
	}
	
	#endregion
}
