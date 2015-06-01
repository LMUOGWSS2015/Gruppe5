using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	protected Transform gazeLight;				//Position of Light
	protected Transform player;					//Position of Player
	protected PlayerHealth playerHealth;
	//EnemyHealth enemyHealth;
	protected NavMeshAgent nav;	
	
	protected void Awake (){
		// Set up the references.
		gazeLight = GameObject.FindGameObjectWithTag ("Light").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}
	
	
	protected void Update (){
		if (/*enemyHealth.currentHealth > 0 &&*/ playerHealth.currentHealth > 0) {
			// set the destination of the nav mesh agent to the player.
			Move ();
		} else {
		// disable the nav mesh agent.
			nav.enabled = false;
		}
	} 

	protected virtual void Move(){

	}
}
