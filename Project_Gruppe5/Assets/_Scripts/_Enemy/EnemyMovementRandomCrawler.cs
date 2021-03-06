﻿using UnityEngine;
using System.Collections;

public class EnemyMovementRandomCrawler : EnemyMovement {
	public enum Follow{RandomOnly, GazeLight, Player};
	public Follow follow = Follow.RandomOnly;
	public float walkSpeed = 2f;
	public float distToNewGoal = 1.2f;
	public float rotSlow = 3f;
	
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
				nav.speed = speed;
				nav.SetDestination (followedObj.position);

				Transform a = transform;
				a.rotation  =  Quaternion.LookRotation(followedObj.position);
				Debug.Log(a.rotation);


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
			Vector3 trans = new Vector3 (Random.Range (-20.0f, 20.0f), 0f, Random.Range (-10.0f, 10.0f));
			trans = trans - transform.position;
			trans.y = 0;
			Transform t = transform;
			t.rotation = Quaternion.LookRotation (trans);

		
			StartCoroutine(Rotation(t.rotation, rotSlow));

			
			Vector3 runTo = t.position + t.forward * 10;
			
			NavMesh.SamplePosition (runTo, out hit, 5, 1 << NavMesh.GetAreaFromName ("Walkable"));
		}


		nav.SetDestination (hit.position);
	}
	
	IEnumerator Rotation(Quaternion to, float time) {
		for(var t = 0f; t < 1; t += Time.deltaTime/time) {
			transform.rotation = Quaternion.Lerp(transform.rotation, to, t);

			yield return null;
		}
	}
	
	protected override void OnTriggerEnter (Collider other){
		base.OnTriggerEnter (other);
		
		if(other.gameObject.tag == "Wall"){
			//	Debug.Log("wall");
			NewGoal();
		}
	}
}
