using UnityEngine;
using System.Collections;

public class GunMovement : MonoBehaviour {

	public float speed = 3.0f;
	public float sensitifity = 1.0f;
	private Transform _transform;

	void FixedUpdate () {

		_transform = transform;

		float rotationX;
		float rotationY;

		rotationX = Input.GetAxis("XboxMacRightX") * sensitifity;
		rotationY = Input.GetAxis("XboxMacRightY") * sensitifity;  

		Debug.Log ("X/Y: " + rotationX + " ~ " + rotationY);

		float _angle = Mathf.Atan2(rotationX, rotationY) * (180 / Mathf.PI);

		_transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(new Vector3(0, _angle, 0)), Time.deltaTime * speed);

	}
}
