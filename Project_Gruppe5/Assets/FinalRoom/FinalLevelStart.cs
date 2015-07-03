using UnityEngine;
using System.Collections;

public class FinalLevelStart : MonoBehaviour {

	public Controller controller;
	private int counter = 0;

	void OnTriggerExit () {
		if (counter == 0) {
			controller.GetComponent<FinalLevelController> ().startFinalLevel ();
			counter++;
		}
	}
}
