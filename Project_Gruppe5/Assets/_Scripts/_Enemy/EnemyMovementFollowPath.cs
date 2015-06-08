using UnityEngine;
using System.Collections;

public class EnemyMovementFollowPath : MonoBehaviour {
	public enum Follow{PathOnly, GazeLight, Player};

	public Follow follow = Follow.PathOnly;
	public float distance = 8f;
	public float patrolSpeed = 2f;
	public float followSpeed = 5f; 
	public float waitTime = 5f;
	public Transform[] patrolWayPoints;


	Transform followedObj;
	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	NavMeshAgent nav;	
	float timer;
	int wayPointIndex;
	float currentDist;

	
	void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		if(follow == Follow.GazeLight)
			followedObj = GameObject.FindGameObjectWithTag ("Light").transform;
		if (follow == Follow.Player)
			followedObj = player;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}
	
	
	void Update (){
		if(playerHealth.currentHealth > 0 && follow != Follow.PathOnly)
			currentDist = Vector3.Distance (followedObj.position, transform.position);
		else
			currentDist = distance + 1;

		if (currentDist > distance)
			Move ();
		else if(enemyHealth.currentHealth > 0) 
			nav.SetDestination(followedObj.position);
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
		
		nav.destination = patrolWayPoints[wayPointIndex].position;
	}
}
