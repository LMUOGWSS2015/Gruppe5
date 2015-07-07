using UnityEngine;
using System.Collections;

public class IntroDDoorsOpen : Activatable {
	public float doorSpeed = 7f;
	public bool roomHasEnemies = true;
	
	private bool enemiesDead;
	
	private Transform leftDoor;
	private Transform rightDoor;
	private float leftClosed;
	private float rightClosed; 
	
	private Quaternion leftRotation;
	private Quaternion rightRotation;

	public Light rightlight;
	public Light leftlight;
	
	
	void Awake (){
		leftDoor = GameObject.Find ("DoorLeft").transform;
		rightDoor = GameObject.Find ("DoorRight").transform;
		
		leftRotation = Quaternion.AngleAxis(-70, Vector3.up);
		rightRotation = Quaternion.AngleAxis(70, Vector3.up);
	}
	
	
	void Update () {
		if (rightlight.color == Color.green && leftlight.color == Color.green) {
			leftDoor.rotation = Quaternion.Slerp (leftDoor.rotation, leftRotation, .05f); 
			rightDoor.rotation = Quaternion.Slerp (rightDoor.rotation, rightRotation, .05f);
		}
	}
	
	public void SetEnemiesDead(bool ed){
		enemiesDead = ed;
	}
	
	public bool GetEnemiesDead(){
		return enemiesDead;
	}
}