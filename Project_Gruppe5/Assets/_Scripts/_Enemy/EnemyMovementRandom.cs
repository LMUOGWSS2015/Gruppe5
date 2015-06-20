using UnityEngine;
using System.Collections;

public class EnemyMovementRandom : EnemyMovement {
	public enum Follow{RandomOnly, GazeLight, Player};
	public Follow follow = Follow.RandomOnly;
	public float walkSpeed = 2f;
	public float distToNewGoal = 1.2f;
	public float rotSpeed = 8f;

	Transform followedObj;
	NavMeshHit hit;
	int oldAngle = 0;

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
			float angle = Vector3.Cross(transform.forward, nav.destination).y;
			//Quaternion.Angle(Quaternion.Euler(trans), transform.rotation);
			
			//Debug.Log(angle);
			int a;
			if(angle < -0.1)
				a = -2;
			else if(angle < 0.1)
				a = 0;
			else
				a = 2;

			if(a != oldAngle){
				animator.SetInteger("turn", a);
				oldAngle = a;
			}

			if(currentDist < distance) {
				nav.speed = speed;
				nav.SetDestination (followedObj.position);
			} else {
				NewGoal();
			}
		} else {
			nav.enabled = false;
			return;
		}
	} 

	void NewGoal(){
		nav.speed = walkSpeed;
		if(nav.remainingDistance <= distToNewGoal){//<= float.Epsilon){
			Vector3 trans = new Vector3 (Random.Range (-17.0f, 17.0f), 0f, Random.Range (-7.0f, 7.0f));
//			trans = trans;// + transform.position;
			trans.y = 0;
//			Debug.Log(trans);
			
			NavMesh.SamplePosition (trans, out hit, 1f, 1 << NavMesh.GetAreaFromName ("Walkable"));
			nav.SetDestination (hit.position);
		}
		nav.SetDestination (hit.position);
	}

	protected override void OnTriggerEnter (Collider other){
		base.OnTriggerEnter (other);

		if(other.gameObject.tag == "Wall"){
			Debug.Log("wall " + transform.position);
			NewGoal();
		}
	}
}
