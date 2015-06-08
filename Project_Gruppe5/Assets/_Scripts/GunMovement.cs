using UnityEngine;
using System.Collections;

public class GunMovement : MonoBehaviour {

	public PlayerMovement playerMovement;

	public float speed = 3.0f;
	public float sensitifity = 1.0f;
	private Transform _transform;

	void Start(){
	
		playerMovement = GameObject.Find ("Player").GetComponent<PlayerMovement>();

	}

	void FixedUpdate () {

		_transform = transform;

		float rotationX;
		float rotationY;

		if(playerMovement.playWithControllerMac){
			rotationX = Input.GetAxis ("XboxMacRightX") * sensitifity;
			rotationY = Input.GetAxis ("XboxMacRightY") * sensitifity;
		}
		else if (playerMovement.playWithControllerWin) {
			rotationX = Input.GetAxis ("XboxWinRightX") * sensitifity;
			rotationY = Input.GetAxis ("XboxWinRightY") * sensitifity;
			
		}
		else {
			rotationX = 0;
			rotationY = 0;
		}

//		Debug.Log ("X/Y: " + rotationX + " ~ " + rotationY);

		float _angle = Mathf.Atan2(rotationX, rotationY) * (180 / Mathf.PI);

		if (!(rotationY == 0f && rotationX == 0)) {
			_transform.rotation = Quaternion.Lerp (_transform.rotation, Quaternion.Euler (new Vector3 (0, _angle, 0)), Time.deltaTime * speed);
		}
	}
}
