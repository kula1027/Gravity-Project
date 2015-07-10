using UnityEngine;
using System.Collections;

public class StartMenuButton : MonoBehaviour {

	public void OnStartGameBtnClick(string scName){
		Application.LoadLevel (scName);
	}

	public void OnExitGameBtnClick(){
		Application.Quit ();
	}
}
