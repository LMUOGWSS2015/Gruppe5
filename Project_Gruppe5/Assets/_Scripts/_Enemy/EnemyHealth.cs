using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 8;	            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	//public AudioClip deathClip;                 // The sound to play when the enemy dies.
	
	
	//Animator anim;                              // Reference to the animator.
	//AudioSource enemyAudio;                     // Reference to the audio source.
	CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
	bool isDead;                                // Whether the enemy is dead.

	
	void Awake (){
		// Setting up the references.
	//	anim = GetComponent <Animator> ();
	//	enemyAudio = GetComponent <AudioSource> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
		
		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}
	
	
	public void TakeDamage (int amount){
		if(isDead)
			return;
		
		// Play the hurt sound effect.
		//enemyAudio.Play ();
		
		currentHealth -= amount;
		
		if(currentHealth <= 0){
			Death ();
		}
	}
	
	
	void Death () {
		isDead = true;
		
		// Turn the collider into a trigger so shots can pass through it.
		capsuleCollider.isTrigger = true;
		
		// Tell the animator that the enemy is dead.
		//anim.SetTrigger ("Dead");
		
		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		Destroy(this.gameObject);
	}
}