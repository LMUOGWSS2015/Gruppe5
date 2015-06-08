using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public float distance = 8f;
	public float speed = 5f;
	public bool freezeInLight = false;

	protected Transform gazeLight;				//Position of Light
	protected Transform player;					//Position of Player
	protected PlayerHealth playerHealth;
	protected EnemyHealth enemyHealth;
	protected NavMeshAgent nav;	

	protected bool frozen;
	
	protected void Awake (){
		gazeLight = GameObject.FindGameObjectWithTag ("Light").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		nav.speed = speed;
	}
	
	
	protected void Update (){
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			Move ();
		} else {
			nav.enabled = false;
		}
	} 

	protected virtual void Move(){

	}

	void OnTriggerEnter (Collider other){
		if(freezeInLight && other.gameObject.tag == "Light"){
			frozen = true;
		}
	}
	
	void OnTriggerExit (Collider other){
		if(freezeInLight && other.gameObject.tag == "Light"){
			frozen = false;
		}
	}
}
