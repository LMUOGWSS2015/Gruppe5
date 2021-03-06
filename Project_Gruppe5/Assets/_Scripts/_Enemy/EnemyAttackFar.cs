﻿using UnityEngine;
using System.Collections;

public class EnemyAttackFar : MonoBehaviour {
//	public bool canMiss = true;
	public int dmgAmnt = 1;
	public float timeBetweenBullets = 0.15f;
	public float range = 8f;
//	public float flashIntensity = 3f; 
//	public float flashFadeSpeed = 3f;
//	public float effectsDisplayTime = 0.1f;
	public GameObject enemyBullet;
	public float height = 1f;

	float timer;
/*	LineRenderer gunLine; 
	Light gunLight; */
	PlayerHealth playerHealth; 
	GameObject player;
	Transform playerPos;
	Transform gunPos;
	Animator animator;

	Ray shootRay;  
	RaycastHit shootHit;
	int mask;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		gunPos = transform.FindChild("Gun");

		playerPos = player.transform;
		playerHealth = player.gameObject.GetComponent<PlayerHealth> ();
	//	gunLine = GetComponentInChildren <LineRenderer> ();
	//	gunLight = gunLine.gameObject.GetComponent<Light>();
		animator = GetComponentInChildren <Animator> ();
		timer = timeBetweenBullets;

	//	gunLine.enabled = false;
	//	gunLight.intensity = 0f;

		mask = LayerMask.GetMask ("Shootable");
	}
	
	void Update(){
		timer += Time.deltaTime;

		if (timer >= timeBetweenBullets && playerHealth != null && Vector3.Distance (playerPos.position, transform.position) <= range) {
			shootRay.origin = transform.position + Vector3.up * 1f;
			shootRay.direction = transform.forward;

			if (Physics.Raycast (shootRay, out shootHit, range, mask)) {
				if (shootHit.transform.gameObject == player)
//				    || Vector3.Distance (shootHit.transform.position, transform.position)
//				    > Vector3.Distance (player.transform.position, transform.position)){
				    Shoot ();
			} else {
				Shoot ();
			}
		} 

//		if(timer >= timeBetweenBullets * effectsDisplayTime) {
			//DisableEffects ();
//		}
		//gunLight.intensity = Mathf.Lerp(gunLight.intensity, 0f, flashFadeSpeed * Time.deltaTime);
	}

	void Shoot(){
		animator.SetTrigger ("shoot");
		timer = 0f;
		//Instantiate (enemyBullet, transform.position + Vector3.up*height, this.transform.rotation);
		//Use the GunPos if it is set.
		if(gunPos != null)Instantiate (enemyBullet, gunPos.position, this.transform.rotation);
		else{
			Instantiate (enemyBullet, transform.position + Vector3.up*height, this.transform.rotation);
		}

//		shootRay.origin = transform.position;
//		shootRay.direction = transform.forward;
//			
//		gunLine.SetPosition (0, gunLine.transform.position);
//
//		//Debug.DrawRay(shootRay.origin, shootRay.direction, Color.white, 3.0f, true);
//
//		if (Physics.Raycast (shootRay, out shootHit, range, mask)) {
////			Debug.Log(shootHit.collider.gameObject);
//			gunLine.SetPosition (1, shootHit.point + Vector3.up * 1.2f);
//
//			if (shootHit.transform.gameObject == player) {
//				playerHealth.TakeDamage (dmgAmnt);
//
//				gunLight.intensity = flashIntensity;
//				gunLine.enabled = true;
//			}
//			if (canMiss) {
//				gunLight.intensity = flashIntensity;
//				gunLine.enabled = true;
//			}
//		} else if(canMiss) {
//			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
//			gunLight.intensity = flashIntensity;
//			
//			gunLine.enabled = true;
//		}
	}

//	void DisableEffects(){
		//gunLine.enabled = false;
		//gunLight.enabled = false;
//	}
}
