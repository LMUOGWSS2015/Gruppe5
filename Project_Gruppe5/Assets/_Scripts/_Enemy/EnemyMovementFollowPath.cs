using UnityEngine;
using System.Collections;

public class EnemyMovementFollowPath : MonoBehaviour {
	public enum Follow{PathOnly, GazeLight, Player};
	
	public bool freezeInLight = false;

	public Follow follow = Follow.PathOnly;
	public float distance = 8f;
	public float patrolSpeed = 2f;
	public float followSpeed = 5f; 
	public float waitTime = 5f;
	public Transform[] patrolWayPoints;


	Animator animator;
	Transform followedObj;
	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	NavMeshAgent nav;	
	float timer;
	int wayPointIndex;
	float currentDist;

	bool frozen;
	
	void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		if(follow == Follow.GazeLight)
			followedObj = GameObject.FindGameObjectWithTag ("Light").transform;
		if (follow == Follow.Player)
			followedObj = player;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		frozen = false;
		animator = GetComponentInChildren<Animator>();
	}
	
	
	void Update (){
		if(playerHealth.currentHealth > 0 && follow != Follow.PathOnly)
			currentDist = Vector3.Distance (followedObj.position, transform.position);
		else
			currentDist = distance + 1;

		if (currentDist > distance)
			Move ();
		else if (enemyHealth.currentHealth > 0 && !frozen) 
			nav.SetDestination (followedObj.position);
		else if (frozen)
			nav.SetDestination (transform.position);
		else
			nav.enabled = false;
	} 

	void Move () {
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

	
	void OnTriggerEnter (Collider other){
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
