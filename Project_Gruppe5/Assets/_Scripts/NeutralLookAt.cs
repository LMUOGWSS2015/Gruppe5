using UnityEngine;
using System.Collections;

public class NeutralLookAt : MonoBehaviour {
	/*public float distance = 8;
	public float speed = 0.01f;

	protected Transform gazeLight;
	EnemyHealth enemyHealth;
	Animator anim;*/
	public float rotSpeed = 5f;
	private Transform player;
	private PlayerHealth playerHealth;

	
	protected void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
	}
	
	
	protected void Update (){
		if(playerHealth.currentHealth > 0)
			Rotate (player, true);

	}
	
	protected void Rotate(Transform t, bool player){
		Vector3 dir = t.position - transform.position;
		dir.y = 0;
		
		if(dir != Vector3.zero)
			StartCoroutine(Rotation(Quaternion.LookRotation(dir), rotSpeed));
		//		if(player ){&& Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir)) < Mathf.Epsilon){
		//			eaf.enabled = true;
		//		}
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

}
