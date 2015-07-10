using UnityEngine;
using System.Collections;

public class StartMenuManager : MonoBehaviour {
	GameObject DroppingBall;
	void Start () {
		DroppingBall = GameObject.Find ("sphere");
		Physics2D.gravity = new Vector2 (10, 0f);
		StartCoroutine ("BallDropper");
	}

	IEnumerator BallDropper(){
		while (true) {
			DroppingBall.transform.position = new Vector2 (-16, 3);
			DroppingBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			yield return new WaitForSeconds (Random.Range (7, 15));
		}
	}
}
