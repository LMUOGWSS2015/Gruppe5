using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour {

	public float speed = 3.0f;
	public bool playWithControllerMac = false;
	public bool playWithControllerWin = false;

	void FixedUpdate() {

		float translationX;
		float translationY;
		float rotationX;
		float rotationY;


		if (playWithControllerMac) {
			translationX = Input.GetAxis ("XboxMacLeftY") * speed;
			translationY = Input.GetAxis ("XboxMacLeftX") * speed;

			rotationX = Input.GetAxis ("XboxMacRightX");
			rotationY = Input.GetAxis ("XboxMacRightY");

<<<<<<< HEAD

=======
>>>>>>> c85ec3bab234c06d31a8c2520432cf34ab1a669f
		}
		else if (playWithControllerWin) {
			translationX = Input.GetAxis ("XboxWinLeftY") * speed;
			translationY = Input.GetAxis ("XboxWinLeftX") * speed;

			rotationX = Input.GetAxis ("XboxWinRightX");
			rotationY = Input.GetAxis ("XboxWinRightY");
<<<<<<< HEAD

=======
>>>>>>> c85ec3bab234c06d31a8c2520432cf34ab1a669f
			
		}
		else {
			translationX = Input.GetAxis ("Vertical") * speed;
			translationY = Input.GetAxis ("Horizontal") * speed;
		}

		translationX *= Time.deltaTime;
		translationY *= Time.deltaTime;
		transform.Translate(translationY, 0, translationX);
	}
}