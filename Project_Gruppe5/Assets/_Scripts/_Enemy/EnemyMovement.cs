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

	protected bool frozen = false;
	
	protected Animator animator;
	
	protected virtual void Awake (){
		gazeLight = GameObject.FindGameObjectWithTag ("Light").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		animator = GetComponentInChildren<Animator>();
		nav = GetComponent <NavMeshAgent> ();
		nav.speed = speed;
	}
	
	
	protected virtual void Update (){
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			animator.SetTrigger ("walking");
			Move ();
		} else {
			animator.SetTrigger ("idle");

			nav.enabled = false;
		}
	} 

	protected virtual void Move(){

	}

	protected virtual void OnTriggerEnter (Collider other){
		if(freezeInLight && other.gameObject.tag == "Light"){
			frozen = true;
			animator.SetBool("frozen",frozen);
		}
	}
	
	void OnTriggerExit (Collider other){
		if(freezeInLight && other.gameObject.tag == "Light"){
			frozen = false;
			animator.SetBool("frozen",frozen);
		}
	}
}
