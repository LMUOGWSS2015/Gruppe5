﻿using UnityEngine;
using System.Collections;

public class DoubleDoorsOpen : Activatable {
	public float doorSpeed = 7f;
	public bool roomHasEnemies = true;
	public AudioClip doorOpenSound;
	
	private bool enemiesDead;

	private bool opened = false;
	
	private Transform leftDoor;
	private Transform rightDoor;
	private float leftClosed;
	private float rightClosed; 
	
	private Quaternion leftRotation;
	private Quaternion rightRotation; 

	public Light leftLight;
	public Light rightLight;
	
	
	void Awake (){
		leftDoor = GameObject.Find ("DoorLeft").transform;
		rightDoor = GameObject.Find ("DoorRight").transform;



		leftRotation = Quaternion.AngleAxis(-70, Vector3.up);
		rightRotation = Quaternion.AngleAxis(70, Vector3.up);
	}
	
	
	void Update () {
		if (!roomHasEnemies || enemiesDead) {
			if(!opened) {
				this.GetComponent<AudioSource>().PlayOneShot(doorOpenSound);
				opened=true;
			}

			leftLight.color = Color.green;
			rightLight.color = Color.green;

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