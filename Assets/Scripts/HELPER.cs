using UnityEngine;
using System.Collections;

public static class HELPER {
	public static Vector2 getDir_Right(Vector3 gravity){//right direction
		return Vector3.Cross (gravity, new Vector3 (0, 0, -1)).normalized;
	}
}
