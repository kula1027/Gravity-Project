using UnityEngine;
using System.Collections;

public class GravityControl : MonoBehaviour {
	private int g_currentMode;
	public int G_currentMode {
		get {
			return g_currentMode;
		}
		set{
			g_currentMode = value;
		}
	}

	private bool g_onChange;
	public bool G_onChange {
		get {
			return g_onChange;
		}
		set {
			g_onChange = value;
		}
	}

	void Start () {
		Physics2D.gravity = new Vector2 (0, -CONFIG.gValue);
		g_currentMode = 0;
		g_onChange = false;
	}

	void GravityConvert(int _g_mode){
		g_onChange = false;

		switch (_g_mode) {
		case 0:
			Physics2D.gravity = new Vector2 (0, -CONFIG.gValue);
			break;
		case 1:
			Physics2D.gravity = new Vector2 (CONFIG.gValue, 0);
			break;
		case 2:
			Physics2D.gravity = new Vector2 (0, CONFIG.gValue);
			break;
		case 3:
			Physics2D.gravity = new Vector2 (-CONFIG.gValue, 0);
			break;
		}

		g_currentMode = _g_mode;
		Debug.Log ("Current Gravity Direction: " + Physics2D.gravity);
	}
}
