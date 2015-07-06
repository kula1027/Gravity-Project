using UnityEngine;
using System.Collections;

public class GravityControl : MonoBehaviour {
	public int g_currentMode;
	public bool g_onChange;
	const float gValue = 42;
	void Start () {
		Physics2D.gravity = new Vector2 (0, -gValue);
		g_currentMode = 0;
		g_onChange = false;
	}

	void GravityConvert(int _g_mode){
		switch (_g_mode) {
		case 0:
			Physics2D.gravity = new Vector2 (0, -gValue);
			break;
		case 1:
			Physics2D.gravity = new Vector2 (gValue, 0);
			break;
		case 2:
			Physics2D.gravity = new Vector2 (0, gValue);
			break;
		case 3:
			Physics2D.gravity = new Vector2 (-gValue, 0);
			break;
		}
		g_currentMode = _g_mode;
		Debug.Log ("Current Gravity Direction: " + Physics2D.gravity);
	}
}
