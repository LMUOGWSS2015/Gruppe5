using UnityEngine;
using System.Collections;

public class EnemyMovementRotOnly : EnemyMovement {
	/*public float distance = 8;
	public float speed = 0.01f;

	protected Transform gazeLight;
	protected Transform player;
	protected PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	Animator anim;*/
	
	protected override void Awake (){
		gazeLight = GameObject.FindGameObjectWithTag ("Light").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		if (enemyHealth == null)
			enemyHealth = GetComponentInChildren<EnemyHealth> ();
		animator = GetComponentInChildren<Animator>();
	}
	
	
	protected override void Update (){
		if (frozen) {
			return;
		} else if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			float plDist = Vector3.Distance (player.position, transform.position);
			float gLDist = Vector3.Distance (gazeLight.position, transform.position);

			if(plDist <= distance && plDist < gLDist)
				Rotate(player, true);
			else if(gLDist <= distance)
				Rotate(gazeLight, false);
			else
				Idle();
		} else if(enemyHealth.currentHealth > 0)
			Idle();
	}

	protected void Rotate(Transform t, bool player){
		if (player)
			animator.SetTrigger ("shoot");
		else
			animator.SetTrigger ("focus");
		Vector3 dir = t.position - transform.position;
		dir.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.time * speed);
	}

	protected void Idle(){
		animator.SetTrigger ("seek");
	}
}
