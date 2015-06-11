using UnityEngine;
using System.Collections;


public class EnemyAttackNear : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int attackDamage = 1;               // The amount of health taken away per attack.
	
	
	//Animator anim;                             
	GameObject player;                    
	PlayerHealth playerHealth;             
	EnemyHealth enemyHealth;                
	bool playerInRange;                       
	float timer;                    
	
	
	void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
	//	anim = GetComponent <Animator> ();
	}
	
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject == player){
			playerInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other){
		if(other.gameObject == player){
			playerInRange = false;
		}
	}
	
	
	void Update (){
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
		
		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0){
			Attack ();
		}
	}
	
	
	void Attack (){
		// Reset the timer.
		timer = 0f;

		if(playerHealth.currentHealth > 0){
			playerHealth.TakeDamage (attackDamage);
		}
	}
}