using UnityEngine;
using System.Collections;

public class ShowDeathScreen : MonoBehaviour {

	// Use this for initialization
	public void Show (bool show) {
		this.gameObject.SetActive (show);
	}

	public void ToMainMenu(){
		Application.LoadLevel ("mainmenu");
	}

	public void RestartLevel(){
		Application.LoadLevel (Application.loadedLevel);
	}
}
