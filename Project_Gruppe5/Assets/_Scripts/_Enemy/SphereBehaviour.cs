using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereBehaviour : MonoBehaviour {
	public float swarmSpeed = 2f;
	public float maxSpeed = 5f;
	public float maxSteer = .05f;

	public float separationWeight = 1f;
	public float alignmentWeight = 1f;
	public float cohesionWeight = 1f;

	public float neighbourhood = 3f;
	public float separation = 1f;
	public float cohesion = 2f;

//	public bool hasGoal = true;
//	public float maxDistanceToGoal = 3f;
//	public Transform goal;

	public List<GameObject> spheres;
	public SwarmIntelligence swarm;

	private Vector3 _separation;
	private Vector3 _alignment;
	private Vector3 _cohesion;

	private Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		Flock ();
	}

	void Flock(){
		Vector3 newVelocity = Vector3.zero;
		
		CalculateVelocities();

		newVelocity += _separation * separationWeight
			+ _alignment * alignmentWeight
			+ _cohesion * cohesionWeight;
		newVelocity *= swarmSpeed;
		newVelocity = rb.velocity + newVelocity;
		newVelocity.y = 0f;
		
		rb.velocity = Limit(newVelocity, maxSpeed);
	}

	void CalculateVelocities(){
		Vector3 separationSum = Vector3.zero;
		Vector3 alignmentSum = Vector3.zero;
		Vector3 cohesionSum = Vector3.zero;
		
		int separationCount = 0;
		int alignmentCount = 0;
		int cohesionCount = 0;
		
		for (int i = 0; i < this.spheres.Count; i++){
			if (spheres[i] == null) continue;

			Rigidbody _rb = spheres[i].GetComponent<Rigidbody>();
			
			float distance = Vector3.Distance(transform.position, spheres[i].transform.position);

			if (distance > 0 && distance < separation) {
				Vector3 direction = transform.position - spheres[i].transform.position;	
				direction.Normalize();
				direction = direction / distance;
				separationSum += direction;
				separationCount++;
			}

			if (distance > 0 && distance < neighbourhood) {
				alignmentSum += _rb.velocity;
				alignmentCount++;
				
				cohesionSum += spheres[i].transform.position;
				cohesionCount++;
			}
		}

		_separation = separationCount > 0 ? separationSum / separationCount : separationSum;
		_alignment = alignmentCount > 0 ? Limit(alignmentSum / alignmentCount, maxSteer) : alignmentSum;
		_cohesion = cohesionCount > 0 ? Steer(cohesionSum / cohesionCount, false) : cohesionSum;
	}

	protected virtual Vector3 Steer(Vector3 target, bool slowDown){
		// the steering vector
		Vector3 steer = Vector3.zero;
		Vector3 targetDirection = target - transform.position;
		float targetDistance = targetDirection.magnitude;
		
		transform.LookAt(target);
		
		if (targetDistance > 0){
			// move towards the target
			targetDirection.Normalize();
			
			// we have two options for speed
			if (slowDown && targetDistance < 100f * swarmSpeed){
				targetDirection *= (maxSpeed * targetDistance / (100f * swarmSpeed));
				targetDirection *= swarmSpeed;
			}
			else{
				targetDirection *= maxSpeed;
			}
			
			// set steering vector
			steer = targetDirection - rb.velocity;
			steer = Limit(steer, maxSteer);
		}
		
		return steer;
	}

	protected virtual Vector3 Limit(Vector3 v, float max){
		if (v.magnitude > max)
			return v.normalized * max;
		else
			return v;
	}
}
