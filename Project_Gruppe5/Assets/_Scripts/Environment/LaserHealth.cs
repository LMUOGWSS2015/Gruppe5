using UnityEngine;

public class LaserHealth : MonoBehaviour {
	public int startingHealth = 3;
	public int currentHealth = 3;
	public GameObject enemyExplosion;
	public float explDuration = 5f; 
	public GameObject bulletExplosion;
	public float bExplDuration = 2f;
	
	
	Animator anim;              
	//AudioSource laserAudio;     
	bool isDead;     
	
	
	void Awake (){
		//anim = GetComponentInChildren <Animator> ();
		//laserAudio = GetComponent <AudioSource> ();
		
		currentHealth = startingHealth;
	}
	
	
	public void TakeDamage (int amount){
		//laserAudio.Play ();
		
		currentHealth -= amount;
		
		if(currentHealth <= 0){
			Destroy (Instantiate (enemyExplosion, this.gameObject.transform.position, Quaternion.identity), explDuration);
			Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Destroy(Instantiate (bulletExplosion, other.gameObject.transform.position, Quaternion.identity),bExplDuration);
			Destroy(other.gameObject);
			if(currentHealth > 0){
				TakeDamage (1);
			}
		}
	}
}