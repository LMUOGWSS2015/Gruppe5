using UnityEngine;
using System.Collections;

public class EnemyMovementRandom_alt : MonoBehaviour {
	public enum Follow{RandomOnly, GazeLight, Player};
	
	public bool freezeInLight = false;
	
	public Follow follow = Follow.RandomOnly;
	public float distance = 8f;
	public float walkSpeed = 5f;
	public float followSpeed = 5f; 

	public float waitTime = 2f;
	
	protected Transform gazeLight;
	protected Transform player;				
	protected PlayerHealth playerHealth;
	protected EnemyHealth enemyHealth;
	protected NavMeshAgent nav;	
	protected NavMeshHit hit;

	protected bool frozen;
	protected bool keepWalking = true;
	
	protected Animator animator;
	protected Transform followedObj;
	
	protected void Awake (){
		gazeLight = GameObject.FindGameObjectWithTag ("Light").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		if(follow == Follow.GazeLight)
			followedObj = GameObject.FindGameObjectWithTag ("Light").transform;
		if (follow == Follow.Player)
			followedObj = player;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		animator = GetComponentInChildren<Animator>();
		nav = GetComponent <NavMeshAgent> ();
		nav.speed = walkSpeed;

		StartCoroutine("RandomMove");
	}

	protected IEnumerator RandomMove(){
		bool keepWalking = true;
		while (keepWalking) {
			float currentDist;

			if (playerHealth.currentHealth > 0 && follow != Follow.RandomOnly)
				currentDist = Vector3.Distance (followedObj.position, transform.position);
			else
				currentDist = distance + 1;

			if (currentDist > distance) {
				Vector3 trans = new Vector3 (Random.Range (-20.0f, 20.0f), 0f, Random.Range (-10.0f, 10.0f));
				trans = trans - transform.position;
				trans.y = 0;
				transform.rotation = Quaternion.LookRotation (trans);
				
				Vector3 runTo = transform.position + transform.forward * 10;
				
				NavMesh.SamplePosition (runTo, out hit, 5, 1 << NavMesh.GetAreaFromName ("Walkable"));
				
				nav.SetDestination (hit.position);
			} else if (enemyHealth.currentHealth > 0 && !frozen) {
				nav.SetDestination (followedObj.position);
			} else if(frozen) {
				nav.SetDestination (transform.position);
			} else {
				nav.enabled = false;
				keepWalking = false;
			}
			yield return new WaitForSeconds (waitTime);
		}

		yield return new WaitForSeconds(waitTime);
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
