using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour {

	public float speed = 3.0f;
	public bool playWithControllerMac = false;
	public bool playWithControllerWin = false;

	void FixedUpdate() {

		float translation;
		float translationH;

		if (playWithControllerMac) {
			translation = Input.GetAxis ("XboxMacLeftY") * speed;
			translationH = Input.GetAxis ("XboxMacLeftX") * speed;

		}
		else if (playWithControllerWin) {
			translation = Input.GetAxis ("XboxWinLeftY") * speed;
			translationH = Input.GetAxis ("XboxWinLeftX") * speed;
			
		}
		else {
			translation = Input.GetAxis ("Vertical") * speed;
			translationH = Input.GetAxis ("Horizontal") * speed;
		}

		translation *= Time.deltaTime;
		translationH *= Time.deltaTime;
		transform.Translate(translationH, 0, translation);
	}
}