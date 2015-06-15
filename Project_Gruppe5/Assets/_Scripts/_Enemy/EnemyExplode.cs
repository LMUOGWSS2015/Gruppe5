using UnityEngine;
using System.Collections;


public class EnemyExplode : MonoBehaviour{
	public int attackDamage = 3;             

	GameObject player;                    
	PlayerHealth playerHealth;              
	EnemyHealth enemyHealth;              
	
	
	void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
	}
	
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject == player){
			playerHealth.TakeDamage (attackDamage);
			enemyHealth.TakeDamage (enemyHealth.currentHealth);
		}
	}
}