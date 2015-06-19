﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 8;
	public int currentHealth = 8;
	public GameObject enemyExplosion;
	//public AudioClip deathClip;    
	public float explDuration = 5f; 
	
	
	Animator anim;              
	//AudioSource enemyAudio;     
	CapsuleCollider capsuleCollider;   
	bool isDead;     

	
	void Awake (){
		anim = GetComponentInChildren <Animator> ();
	//	enemyAudio = GetComponent <AudioSource> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();

		currentHealth = startingHealth;
	}
	
	
	public void TakeDamage (int amount){
		if(isDead)
			return;

		//enemyAudio.Play ();
		
		currentHealth -= amount;
		
		if(currentHealth <= 0){
			Death ();
		}
	}
	
	
	void Death () {
		isDead = true;

		anim.SetTrigger ("Dead");
		capsuleCollider.isTrigger = true;
		
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		Destroy(Instantiate (enemyExplosion, this.gameObject.transform.position, Quaternion.identity),explDuration);
		Debug.Log(this.gameObject.tag);
		if (this.gameObject.tag == "Enemy")
			Destroy (this.gameObject);
		else
			Destroy (this.transform.parent.gameObject);

		transform.GetComponentInParent<Enemies> ().Less(1);
	}
}