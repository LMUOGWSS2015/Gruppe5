using UnityEngine;
using System.Collections;

public class EnemyAttackFar : MonoBehaviour {
	public int dmgAmnt = 1;
	public float timeBetweenBullets = 0.15f;
	public float range = 8f;
	public float flashIntensity = 5f; 
	public float fadeSpeed = 3f;


	float timer;
	LineRenderer gunLine; 
	Light gunLight; 
	PlayerHealth playerHealth; 
	Transform player;
	float effectsDisplayTime = 0.3f;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.gameObject.GetComponent<PlayerHealth> ();
		gunLine = GetComponentInChildren <LineRenderer> ();
		gunLight = gunLine.gameObject.GetComponent<Light>();

		gunLine.enabled = false;
		gunLight.intensity = 0f;
	}
	
	void Update(){
		timer += Time.deltaTime;

		if (timer >= timeBetweenBullets && Vector3.Distance(player.position, transform.position) <= range) {
			Shoot();
		}
		if(timer >= timeBetweenBullets * effectsDisplayTime) {
			DisableEffects ();
		}
		gunLight.intensity = Mathf.Lerp(gunLight.intensity, 0f, fadeSpeed * Time.deltaTime);
	}

	void Shoot(){
		timer = 0f;

		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		if(playerHealth != null){
			playerHealth.TakeDamage (dmgAmnt);
			gunLine.SetPosition(1, player.position + Vector3.up * 1.5f);
		}
	}

	void DisableEffects(){
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
}
