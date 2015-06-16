﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 3;                            	// The amount of health the player starts the game with.
	public int currentHealth = 3;			                    // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	//public AudioClip deathClip;                               // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	
	
	//Animator anim;                                              // Reference to the Animator component.
	//AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	//ShotsFired shotsFired;     			                         // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.

	public GameObject playerHit;
	public GameObject playerExplosion;

	public float explDuration = 1f;
	public float hitDuration = 2f;

	
	ShowDeathScreen sds;
	
	void Awake (){
		//anim = GetComponent <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		//shotsFired = GetComponent <ShotsFired> ();

		currentHealth = startingHealth;

		sds = GameObject.FindGameObjectWithTag ("DeathScreen").GetComponentInChildren<ShowDeathScreen>();
		sds.Show(false);
	}
	
	
	void Update () {
		if(damaged) {
			damageImage.color = flashColour;
		} else {
			// Transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		
		// Reset the damaged flag.
		damaged = false;
	}
	
	
	public void TakeDamage (int amount) {
		// Set the damaged flag so the screen will flash.
		damaged = true;
		
		// Reduce the current health by the damage amount.
		currentHealth -= amount;
		
		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;
		
		// Play the hurt sound effect.
		//playerAudio.Play ();

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead) {
			// ... it should die.
			//Debug.Log("FATAL");
			Death ();
		} else {
			//Debug.Log("Hit");
			Destroy(Instantiate (playerHit, this.gameObject.transform.position, Quaternion.identity),hitDuration);
		}
	}
	
	
	void Death (){
		Destroy(Instantiate (playerExplosion, this.gameObject.transform.position, Quaternion.identity),explDuration);
		isDead = true;
		
		// Tell the animator that the player is dead.
		//anim.SetTrigger ("Die");
		
		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();
		
		// Turn off the movement and shooting scripts.
		playerMovement.enabled = false;
		//shotsFired.enabled = false;
		Destroy (this.gameObject);

		sds.Show(true);
	}    
}