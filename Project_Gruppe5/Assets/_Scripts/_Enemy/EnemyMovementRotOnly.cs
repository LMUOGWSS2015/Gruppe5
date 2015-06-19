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
	public float rotSpeed = 5f;

	private EnemyAttackFar eaf;
	
	protected override void Awake (){
		gazeLight = GameObject.FindGameObjectWithTag ("Light").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		if (enemyHealth == null)
			enemyHealth = GetComponentInChildren<EnemyHealth> ();
		eaf = GetComponent <EnemyAttackFar>();
		eaf.enabled = false;
		animator = GetComponentInChildren<Animator>();
	}
	
	
	protected override void Update (){
		if (frozen) {
			eaf.enabled = false;
		} else if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			float plDist = Vector3.Distance (player.position, transform.position);
			float gLDist = Vector3.Distance (gazeLight.position, transform.position);

			if (plDist <= distance && plDist < gLDist)
				Rotate (player, true);
			else if (gLDist <= distance)
				Rotate (gazeLight, false);
			else
				Idle ();
		} else if (enemyHealth.currentHealth > 0) {
			eaf.enabled = false;
			Idle ();
		}
	}

	protected void Rotate(Transform t, bool player){
		if (!player) {
			eaf.enabled = false;
		}

		animator.SetTrigger ("focus");

		Vector3 dir = t.position - transform.position;
		dir.y = 0;

		StartCoroutine(Rotation(Quaternion.LookRotation(dir), rotSpeed));
		if(player && Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir)) < Mathf.Epsilon){
			eaf.enabled = true;
		}
		// transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.time * speed);
	}
	
	IEnumerator Rotation(Quaternion to, float time) {
		float elapsedTime = 0f;
		while (elapsedTime < time) {
			elapsedTime += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(transform.rotation, to, elapsedTime);
			yield return new WaitForEndOfFrame ();
		}
		yield return null;
	}

	protected void Idle(){
		animator.SetTrigger ("seek");
	}
}
