using UnityEngine;
using System.Collections;

public class EnemyAttackFar : MonoBehaviour {
	public int dmgAmnt = 1;
	public float timeBetweenBullets = 0.15f;
	public float range = 8f;
	public float flashIntensity = 10f; 
	public float fadeSpeed = 3f;


	float timer;
	LineRenderer gunLine; 
	Light gunLight; 
	PlayerHealth playerHealth; 
	GameObject player;
	Transform playerPos;
	float effectsDisplayTime = 0.3f;

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
		gunLight.intensity = Mathf.Lerp(gunLight.intensity, 0f, fadeSpeed * Time.deltaTime);
	}

	void Shoot(){
		timer = 0f;

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
			
		gunLine.SetPosition (0, gunLine.transform.position);
		gunLine.SetPosition (1, playerPos.position);

		Debug.DrawRay(shootRay.origin, shootRay.direction, Color.white, 3.0f, true);

		if (Physics.Raycast (shootRay, out shootHit, range, mask)){
			Debug.Log(shootHit.collider.gameObject);
			if(shootHit.transform.gameObject == player){
				playerHealth.TakeDamage (dmgAmnt);

				gunLight.intensity = flashIntensity;

				gunLine.enabled = true;
			}
		}
	}

	void DisableEffects(){
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
}
