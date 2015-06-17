using UnityEngine;
using System.Collections;

public class EnemyMovementRandom : EnemyMovement {
	public enum Follow{RandomOnly, GazeLight, Player};
	public Follow follow = Follow.RandomOnly;
	public float walkSpeed = 2f;

	Transform followedObj;
	NavMeshHit hit;

	protected override void Awake(){
		base.Awake ();
		
		if(follow == Follow.GazeLight)
			followedObj = GameObject.FindGameObjectWithTag ("Light").transform;
		if (follow == Follow.Player)
			followedObj = player;
	}
	
	protected override void Move (){
		float currentDist;
		if(follow == Follow.GazeLight || (playerHealth.currentHealth > 0 && follow == Follow.Player))
			currentDist = Vector3.Distance (followedObj.position, transform.position);
		else
			currentDist = distance + 1;

		if (frozen){
			nav.SetDestination (transform.position);
		} else if (enemyHealth.currentHealth > 0){
			if(currentDist < distance) {
				Debug.Log("walk");
				nav.speed = speed;
				nav.SetDestination (followedObj.position);
			} else {
				nav.speed = walkSpeed;
				if(nav.remainingDistance <= float.Epsilon){
					Debug.Log("here");
					Vector3 trans = new Vector3 (Random.Range (-20.0f, 20.0f), 0f, Random.Range (-10.0f, 10.0f));
					trans = trans - transform.position;
					trans.y = 0;
					transform.rotation = Quaternion.LookRotation (trans);
					Debug.Log (trans);
					
					Vector3 runTo = transform.position + transform.forward * 10;

					NavMesh.SamplePosition (runTo, out hit, 5, 1 << NavMesh.GetAreaFromName ("Walkable"));
				}
				nav.SetDestination (hit.position);
			}
		} else {
			nav.enabled = false;
			return;
		}
	} 
}
