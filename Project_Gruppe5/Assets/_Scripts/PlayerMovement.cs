using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour {

	public float speed = 3.0f;
	public bool playWithControllerMac = false;
	public bool playWithControllerWin = false;

	void FixedUpdate() {

		float translation;
		float translationH;
		float rotationX;
		float rotationY;


		if (playWithControllerMac) {
			translation = Input.GetAxis ("XboxMacLeftY") * speed;
			translationH = Input.GetAxis ("XboxMacLeftX") * speed;

			rotationX = Input.GetAxis ("XboxMacRightX");
			rotationY = Input.GetAxis ("XboxMacRightY");

			Debug.Log ("X/Y: " + rotationX + " ~ " + rotationY);

		}
		else if (playWithControllerWin) {
			translation = Input.GetAxis ("XboxWinLeftY") * speed;
			translationH = Input.GetAxis ("XboxWinLeftX") * speed;

			rotationX = Input.GetAxis ("XboxWinRightX");
			rotationY = Input.GetAxis ("XboxWinRightY");
			
			Debug.Log ("X/Y: " + rotationX + " ~ " + rotationY);
			
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