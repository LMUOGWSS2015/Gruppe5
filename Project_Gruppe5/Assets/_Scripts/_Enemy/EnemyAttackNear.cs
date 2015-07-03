using UnityEngine;
using System.Collections;


public class EnemyAttackNear : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;   
	public int attackDamage = 1;              
	                      
	GameObject player;                    
	PlayerHealth playerHealth;             
	EnemyHealth enemyHealth;                
	bool playerInRange;                       
	float timer;                    
	
	
	void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
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
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0){
			Attack ();
		}
	}
	
	
	void Attack (){
		timer = 0f;

		if(playerHealth.currentHealth > 0){
			playerHealth.TakeDamage (attackDamage);
		}
	}
}