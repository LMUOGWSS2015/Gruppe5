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

			//TODO 	rotationX = Input.GetAxis ("XboxMacRightX");
			//		rotationY = Input.GetAxis ("XboxMacRightY");

		}
		else if (playWithControllerWin) {
			translation = Input.GetAxis ("XboxWinLeftY") * speed;
			translationH = Input.GetAxis ("XboxWinLeftX") * speed;

			//TODO 	rotationX = Input.GetAxis ("XboxWinRightX");
			//		rotationY = Input.GetAxis ("XboxWinRightY");
			
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