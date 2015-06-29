using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour {

	public float speed = 3.0f;
	public float sensitifity = 1.0f;
	public bool mac = false;
	public bool win = false;
	public bool Controller = false;

	private Transform _transform;
	private Rigidbody rb;

	Animator animator;

	void Start(){
		//Transform upperSpine = transform.find("bones/lowerSpine/upperspine");
		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();

		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) {
			mac = true;
			win = false;
		} else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			mac = false;
			win = true;
		} else {
			mac = false;
			win = true;
		}
	}

	void FixedUpdate() {
	

		if((Input.GetAxis ("XboxMacLeftY") != 0.0f) || (Input.GetAxis ("XboxMacLeftX") != 0.0f) ||
		   (Input.GetAxis ("XboxMacRightY") != 0.0f) || (Input.GetAxis ("XboxMacRightX") != 0.0f) ||
		   (Input.GetAxis ("XboxWinLeftY") != 0.0f) || (Input.GetAxis ("XboxWinLeftX") != 0.0f) ||
		   (Input.GetAxis ("XboxWinRightY") != 0.0f) || (Input.GetAxis ("XboxWinRightX") != 0.0f)) {
			Controller = true;
		}


		float translationX = 0;
		float translationY = 0;
		float shootDirectionX;
		float shootDirectionY;

		_transform = transform;
		shootDirectionX = _transform.rotation.x;
		shootDirectionY = _transform.rotation.y;

		if (mac && Controller) {
			translationX = Input.GetAxis ("XboxMacLeftY");
			translationY = Input.GetAxis ("XboxMacLeftX");

			shootDirectionX = Input.GetAxis ("XboxMacRightY") * sensitifity;
			shootDirectionY = Input.GetAxis ("XboxMacRightX") * sensitifity;

		} else if (win && Controller) {
			translationX = Input.GetAxis ("XboxWinLeftY");
			translationY = Input.GetAxis ("XboxWinLeftX");

			shootDirectionX = Input.GetAxis ("XboxWinRightY") * sensitifity;
			shootDirectionY = Input.GetAxis ("XboxWinRightX") * sensitifity;

		} else {
			translationX = Input.GetAxis ("Vertical");
			translationY = Input.GetAxis ("Horizontal");
		}

	
		//float _angle = Mathf.Atan2(rotationX, rotationY) * 	(180 / Mathf.PI);

	
		if (translationX == 0 && translationY ==0) {
			animator.SetBool("walking",false);

		} else {
			animator.SetBool("walking",true);
		

		}




			Vector3 movement = new Vector3 (translationY, 0, translationX);
		

		Vector3 shooting = new Vector3(shootDirectionY, 0, shootDirectionX);
		_transform.position += movement*speed;
		if (translationX != 0 || translationY != 0) {
		_transform.rotation = Quaternion.LookRotation(movement);
	}
		if (shootDirectionX != 0 || shootDirectionY != 0) {
			_transform.rotation = Quaternion.LookRotation (shooting);
			animator.SetBool ("shooting", true);


		} else {
			animator.SetBool ("shooting", false);

		}
	}
}
