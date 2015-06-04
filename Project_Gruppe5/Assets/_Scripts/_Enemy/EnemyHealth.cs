using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 8;
	public int currentHealth;
	//public AudioClip deathClip;     
	
	
	//Animator anim;              
	//AudioSource enemyAudio;     
	CapsuleCollider capsuleCollider;   
	bool isDead;     

	
	void Awake (){
	//	anim = GetComponent <Animator> ();
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
		
		// Turn the collider into a trigger so shots can pass through it.
		capsuleCollider.isTrigger = true;

		//anim.SetTrigger ("Dead");
		
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		Destroy(this.gameObject);
	}
}