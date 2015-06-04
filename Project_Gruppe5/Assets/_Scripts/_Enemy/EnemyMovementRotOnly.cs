using UnityEngine;
using System.Collections;

public class EnemyMovementRotOnly : MonoBehaviour {
	protected Transform player;
	protected PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	
	protected void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
	}
	
	
	protected void Update (){
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			Vector3 trans = player.position - transform.position;
			trans.y = 0;
			transform.rotation = Quaternion.LookRotation(trans);
		}
	}
}
