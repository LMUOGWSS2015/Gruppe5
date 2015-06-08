using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public float distance = 8f;
	public float speed = 5f;

	protected Transform gazeLight;				//Position of Light
	protected Transform player;					//Position of Player
	protected PlayerHealth playerHealth;
	protected EnemyHealth enemyHealth;
	protected NavMeshAgent nav;	
	
	protected void Awake (){
		gazeLight = GameObject.FindGameObjectWithTag ("Light").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		nav.speed = speed;
	}
	
	
	protected void Update (){
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && Vector3.Distance (player.position, transform.position) <= distance) {
			Move ();
		} else {
			nav.enabled = false;
		}
	} 

	protected virtual void Move(){

	}
}
