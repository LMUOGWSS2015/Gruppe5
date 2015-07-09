using UnityEngine;
using System.Collections;

public class EnemyMovementRandom : EnemyMovement {
	public enum Follow{RandomOnly, GazeLight, Player};
	public Follow follow = Follow.RandomOnly;
	public float walkSpeed = 2f;
	public float distToNewGoal = 1.2f;
	public float rotSpeed = 8f;
	public float rotEpsilon = 0.1f;

	public float[] rangeXXYY = {-15f, 15f, -5f, 5f};


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
			
		//	Debug.Log(angle);
			int a;
			if(angle < -rotEpsilon){
				a = -2;
				nav.speed = speed/5;
			}else if(angle < rotEpsilon){
				a = 0;
				nav.speed = speed;
			}else{
				a = 2;
				nav.speed = speed/5;
			}

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
		if (nav.enabled == true) {
			if (nav.remainingDistance <= distToNewGoal) {//<= float.Epsilon){
				Vector3 trans = new Vector3 (Random.Range (rangeXXYY [0], rangeXXYY [1]), 0f, Random.Range (rangeXXYY [2], rangeXXYY [3]));
				trans.y = 0;
			
				NavMesh.SamplePosition (trans, out hit, 1f, 1 << NavMesh.GetAreaFromName ("Walkable"));
				nav.SetDestination (hit.position);
			}
			nav.SetDestination (hit.position);
		}
	}

//	protected override void OnTriggerEnter (Collider other){
//		base.OnTriggerEnter (other);
//
//		if(other.gameObject.tag == "Wall"){
//			Debug.Log("wall " + transform.position);
//			NewGoal();
//		}
//	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		float x = rangeXXYY[1]-rangeXXYY[0];
		float y = rangeXXYY[3]-rangeXXYY[2];

		Gizmos.DrawWireCube (Vector3.zero, new Vector3 (x,1,y));
	}
}
