using UnityEngine;
using System.Collections;

public class EnemyAttackFar : MonoBehaviour {

	public bool canMiss = true;
	public int dmgAmnt = 1;
	public float timeBetweenBullets = 0.15f;
	public float range = 8f;
	public float flashIntensity = 3f; 
	public float flashFadeSpeed = 3f;
	public float effectsDisplayTime = 0.1f;

	public GameObject enemyBullet;

	float timer;
	LineRenderer gunLine; 
	Light gunLight; 
	PlayerHealth playerHealth; 
	GameObject player;
	Transform playerPos;


	Ray shootRay;  
	RaycastHit shootHit;
	int mask;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerPos = player.transform;
		playerHealth = player.gameObject.GetComponent<PlayerHealth> ();
		gunLine = GetComponentInChildren <LineRenderer> ();
		gunLight = gunLine.gameObject.GetComponent<Light>();

		gunLine.enabled = false;
		gunLight.intensity = 0f;

		mask = LayerMask.GetMask ("Shootable");
	}
	
	void Update(){
		timer += Time.deltaTime;

		if (timer >= timeBetweenBullets && playerHealth != null && Vector3.Distance(playerPos.position, transform.position) <= range) {
			Shoot();
		}
		if(timer >= timeBetweenBullets * effectsDisplayTime) {
			DisableEffects ();
		}
		gunLight.intensity = Mathf.Lerp(gunLight.intensity, 0f, flashFadeSpeed * Time.deltaTime);
	}

	void Shoot(){

		Instantiate (enemyBullet, new Vector3(this.transform.position.x, this.transform.position.y +0.65f, this.transform.position.z), this.transform.rotation);
//
//
		timer = 0f;
//
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

	void DisableEffects(){
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
}
