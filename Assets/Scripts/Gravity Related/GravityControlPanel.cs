using UnityEngine;
using System.Collections;

public class GravityControlPanel : MonoBehaviour {
	private bool activated;
	private GameObject player;
	private GravityControl g_control;

	void Start(){
		activated = false;
		player = GameObject.Find ("Player");

		g_control = GameObject.Find ("GravityControl").GetComponent<GravityControl> ();
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.Equals(player)) {
			activated = true;
			Debug.Log (gameObject.name + " Activated");
		}
	}

	Vector2 g_direction;
	int g_mode = 0;
	void OnTriggerStay2D(Collider2D coll){
		if (activated && !g_control.G_onChange) {
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

	int GetGmode(Vector2 g_direction){
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

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.Equals(player)) {
			activated = false;
			Debug.Log (gameObject.name + " Deactivated");
		}
	}
}
