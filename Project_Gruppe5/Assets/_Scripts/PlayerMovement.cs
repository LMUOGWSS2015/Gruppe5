using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour {

	public float speed = 3.0f;

	void FixedUpdate() {
		float translation = Input.GetAxis("Vertical") * speed;
		float translationH = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		translationH *= Time.deltaTime;
		transform.Translate(translationH, 0, translation);
	}
}