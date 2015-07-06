﻿using UnityEngine;
using System.Collections;

public class IntroDDoorsLowerOpen : MonoBehaviour {
	public float doorSpeed = 7f;
	public float startGameDoorDist = 3f;
	
	private Transform leftDoor;
	private Transform rightDoor;

	private GameObject button;

	private float leftClosed;
	private float rightClosed; 
	
	private Quaternion leftRotation;
	private Quaternion rightRotation; 
	
	private Quaternion leftRotationClose;
	private Quaternion rightRotationClose;
	
	//	private bool isLowerDoors = false;
	private bool closing = false;

	public GameObject manTV;
	public GameObject womanTV;
	private Renderer rendererMan;
	private Renderer rendererWoman;

	public Texture manTexture;
	public Texture womanTexture;
	public Texture blankTexture;

	private PlayerMovement player;

	AudioSource audio;
	

	void Awake (){

		audio = GetComponent<AudioSource>();
		rendererMan = manTV.GetComponent<Renderer> ();
		rendererWoman = womanTV.GetComponent<Renderer> ();
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		//		if (this.gameObject.tag == "DDoorsLower") {
		//			isLowerDoors = true;
		//		}
		
		//		if (!isLowerDoors) {
		leftDoor = GameObject.Find ("DoorLeftLower").transform;
		rightDoor = GameObject.Find ("DoorRightLower").transform;
		button = GameObject.Find ("Button");
		//		}
		//		else if (isLowerDoors) {
		//			leftDoor = GameObject.Find ("DoorLeft").transform;
		//			rightDoor = GameObject.Find ("DoorRight").transform;
		//		}
		
		leftRotation = Quaternion.AngleAxis (-70, Vector3.up);
		rightRotation = Quaternion.AngleAxis (70, Vector3.up);
		
	}
	
	public void closeDoors () {
		audio.Play();
		closing = true;
		leftRotationClose = Quaternion.AngleAxis(-0, Vector3.down);
		rightRotationClose = Quaternion.AngleAxis(0, Vector3.down);

		GameObject.Find ("Spotlight").GetComponent<CapsuleCollider> ().enabled = true;
	}
	
	
	void Update () {

		if (!closing) {
			leftDoor.rotation = Quaternion.Slerp (leftDoor.rotation, leftRotation, .05f); 
			rightDoor.rotation = Quaternion.Slerp (rightDoor.rotation, rightRotation, .05f);
		}
		if (closing) {

			leftDoor.rotation = Quaternion.Slerp (leftDoor.rotation, leftRotationClose, .05f); 
			rightDoor.rotation = Quaternion.Slerp (rightDoor.rotation, rightRotationClose, .05f);

		}
			
			if(Quaternion.Angle(leftDoor.rotation, leftRotationClose) < Mathf.Epsilon && !audio.isPlaying){

			rendererMan.material.SetTexture(0,manTexture);
			rendererWoman.material.SetTexture(0,womanTexture);


			}

		if (player.male) {

			rendererWoman.material.SetTexture(0,blankTexture);
			GameObject.Find("IntroController").GetComponent<IntroLevel>().StartLevel(true);
			button.transform.position = new Vector3(0f, 0.57f, 6.52f);

		}
		if (player.female) {
			rendererMan.material.SetTexture(0,blankTexture);
			GameObject.Find("IntroController").GetComponent<IntroLevel>().StartLevel(true);
			button.transform.position = new Vector3(0f, 0.57f, 6.52f);
			button.transform.position = new Vector3(
				transform.position.x + (2f * Time.deltaTime),
				transform.position.y,
				transform.position.z
				);
			
		}
	}
	}
