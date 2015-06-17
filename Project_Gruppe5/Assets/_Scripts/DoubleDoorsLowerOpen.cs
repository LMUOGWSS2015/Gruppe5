using UnityEngine;
using System.Collections;

public class DoubleDoorsLowerOpen : MonoBehaviour {
	public float doorSpeed = 7f;
	
	public bool enemiesDead;
	
	private Transform leftDoor;
	private Transform rightDoor;
	private float leftClosed;
	private float rightClosed; 
	
	private Quaternion leftRotation;
	private Quaternion rightRotation; 
	
	private Quaternion leftRotationClose;
	private Quaternion rightRotationClose;
	
	private bool isLowerDoors = false;
	private bool closing = false;
	
	void Awake (){
		if (this.gameObject.tag == "DDoorsLower") {
			isLowerDoors = true;
		}
		
		//		if (!isLowerDoors) {
		leftDoor = GameObject.Find ("DoorLeftLower").transform;
		rightDoor = GameObject.Find ("DoorRightLower").transform;
		//		}
		//		else if (isLowerDoors) {
		//			leftDoor = GameObject.Find ("DoorLeft").transform;
		//			rightDoor = GameObject.Find ("DoorRight").transform;
		//		}
		
		leftRotation = Quaternion.AngleAxis (-90, Vector3.up);
		rightRotation = Quaternion.AngleAxis (90, Vector3.up);
	
	}

	public void closeDoors () {
		closing = true;
		leftRotationClose = Quaternion.AngleAxis(-0, Vector3.down);
		rightRotationClose = Quaternion.AngleAxis(0, Vector3.down);
	}
	
	
	void Update () {
		if ((enemiesDead || isLowerDoors) && !closing) {
			leftDoor.rotation = Quaternion.Slerp (leftDoor.rotation, leftRotation, .05f); 
			rightDoor.rotation = Quaternion.Slerp (rightDoor.rotation, rightRotation, .05f);
		}
		if (closing) {
			leftDoor.rotation = Quaternion.Slerp (leftDoor.rotation, leftRotationClose, .05f); 
			rightDoor.rotation = Quaternion.Slerp (rightDoor.rotation, rightRotationClose, .05f);

			if(Quaternion.Angle(leftDoor.rotation, leftRotationClose) < Mathf.Epsilon){
				GameObject.Find("Controller").GetComponent<Level>().StartLevel(true);
			}
		}
	}
}