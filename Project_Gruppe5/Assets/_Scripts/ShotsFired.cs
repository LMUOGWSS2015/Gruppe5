﻿using UnityEngine;
using System.Collections;

public class ShotsFired : MonoBehaviour {

	public AudioClip[] shotSounds;
	public GameObject bullet;
	private float rightTrigger;
	int counter = 0;
	public int shotSpeed = 15;
	private GameObject[] bulletExplosion;
	private GameObject[] enemyExplosion;

	private PlayerMovement playerMovement;
	Animator animator;
	private Transform leftGun;
	private Transform rightGun;

	private bool shootLeft = true;

	void Start(){
		animator = GameObject.Find ("roboBuddy").GetComponent<Animator>();
		playerMovement = GameObject.Find ("Player").GetComponent<PlayerMovement>();
		leftGun = GameObject.Find ("left").GetComponent<Transform>();
		rightGun = GameObject.Find ("right").GetComponent<Transform>();
	}

	void FixedUpdate(){

		if (playerMovement.mac) {

			rightTrigger = Input.GetAxis ("XboxMacRightTrigger");
			counter++;
		}
		else if(playerMovement.win){

			rightTrigger = Input.GetAxis ("XboxWinRightTrigger");
			counter++;

		}

		if (rightTrigger < 0){
			if (!animator.GetNextAnimatorStateInfo(0).IsName("shooting"))
				animator.SetBool ("shooting", true);
			else 
				animator.SetBool ("shooting", false);

			animator.SetBool ("firing", true);
/*			if (counter > shotSpeed) {
				counter = 0;
				shoot ();
				}*/
		}else{
			animator.SetBool ("firing", false);
		}
	


	}
	
	void Update () {


		if (Input.GetKeyDown (KeyCode.Space)) {

			Instantiate (bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);

			this.GetComponent<AudioSource> ().PlayOneShot(shotSounds[Random.Range(0, shotSounds.Length)]);

			bulletExplosion = GameObject.FindGameObjectsWithTag("bulletExplosion");
			enemyExplosion = GameObject.FindGameObjectsWithTag("enemyExplosion");
			foreach (GameObject obj in bulletExplosion) {
				Destroy(obj);
			}
			foreach (GameObject obj in enemyExplosion) {
				Destroy(obj);
			}
		}
	}

	void shoot(){
		 this.GetComponent<AudioSource> ().PlayOneShot(shotSounds[Random.Range(0, shotSounds.Length)]);
		Debug.Log ("shotsounds");


		if (shootLeft) {
			Instantiate (bullet, new Vector3 (leftGun.position.x, leftGun.position.y, leftGun.position.z), leftGun.rotation);

		} else {
			Instantiate (bullet, new Vector3 (rightGun.position.x, rightGun.position.y, rightGun.position.z), rightGun.rotation);

		}

		shootLeft = ! shootLeft;
	}
}
