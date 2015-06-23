using UnityEngine;
using System.Collections;

public class EnemyMovementFollowPath : EnemyMovement {
	public enum Follow{PathOnly, GazeLight, Player};
	
	//public bool freezeInLight = false;

	public Follow follow = Follow.PathOnly;
	//public float distance = 8f;
	public float patrolSpeed = 2f;
	//public float followSpeed = 5f; 
	public float waitTime = 5f;
	public Transform[] patrolWayPoints;


//	Animator animator;
	Transform followedObj;
//	Transform player;
//	PlayerHealth playerHealth;
//	EnemyHealth enemyHealth;
//	NavMeshAgent nav;	
	float timer;
	int wayPointIndex;
	float currentDist;
	
	Ray shootRay;  
	RaycastHit shootHit;

//	bool frozen;
	
	protected override void Awake (){
		base.Awake ();
//		player = GameObject.FindGameObjectWithTag ("Player").transform;
		if(follow == Follow.GazeLight)
			followedObj = gazeLight;
		if (follow == Follow.Player)
			followedObj = player;
//		playerHealth = player.GetComponent <PlayerHealth> ();
//		enemyHealth = GetComponent <EnemyHealth> ();
//		nav = GetComponent <NavMeshAgent> ();
//		frozen = false;
//		animator = GetComponentInChildren<Animator>();
	}
	

	protected override void Update (){
		if (follow == Follow.GazeLight || (playerHealth.currentHealth > 0 && follow == Follow.Player)) {
			nav.speed = speed;
			currentDist = Vector3.Distance (followedObj.position, transform.position);
		} else
			currentDist = distance + 1;

		if (frozen)
			nav.SetDestination (transform.position);
		else if (currentDist > distance && enemyHealth.currentHealth > 0)
			Move ();
		else if (enemyHealth.currentHealth > 0) {
			shootRay.origin = transform.position;
			shootRay.direction = player.transform.position;
			
			if (Physics.Raycast (shootRay, out shootHit, distance)) {
				if (shootHit.transform.gameObject == player)
					nav.SetDestination (followedObj.position);
				else Move ();
			}
		} else {
			nav.enabled = false;
			animator.SetTrigger ("idle");
		}
	} 

	protected override void Move () {
		animator.SetTrigger ("walking");
		nav.speed = patrolSpeed;

		if(nav.remainingDistance < nav.stoppingDistance){
			timer += Time.deltaTime;
			
			if(timer >= waitTime) {
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;

				timer = 0;
			}
		}
		else
			timer = 0;

		if (frozen)
			nav.destination = transform.position;
		else
			nav.destination = patrolWayPoints[wayPointIndex].position;
	}

	
	/*void OnTriggerEnter (Collider other){
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
	}*/
}
