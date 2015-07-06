using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 8;
	public int currentHealth = 8;
	public GameObject enemyExplosion;
	//public AudioClip deathClip;    
	public float explDuration = 5f; 
	
	 
	//AudioSource enemyAudio;       
	bool isDead;     

	
	void Awake (){
	//	enemyAudio = GetComponent <AudioSource> ();

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

		
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		Destroy (Instantiate (enemyExplosion, this.gameObject.transform.position, Quaternion.identity), explDuration);
	
		if (transform.parent.gameObject.tag == "Enemy") {
			Destroy (this.transform.parent.gameObject);
		} else {
			Destroy (this.gameObject);
		}

		transform.GetComponentInParent<Enemies> ().Less(1);
	}
}