using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

}
