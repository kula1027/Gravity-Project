using UnityEngine;
using System.Collections;

public class ExitPortal : MonoBehaviour {
	public string NextStage;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.name.Equals ("Player")) {
			StartCoroutine("LoadSceneInSeconds", 3);
		}
	}

	IEnumerator LoadSceneInSeconds(int secs){
		yield return new WaitForSeconds(secs);

		Application.LoadLevel (NextStage);
	}
}
