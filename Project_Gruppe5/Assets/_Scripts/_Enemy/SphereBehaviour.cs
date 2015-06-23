using UnityEngine;
using System.Collections;

public class SphereBehaviour : MonoBehaviour {
	public float currentSpeed = 2f;
	public float maxSpeed = 5f;
	public float seperation = 1f;
	public float cohesion = 2f;
	
	public bool hasGoal = true;
	public float maxDistanceToGoal = 3f;
	public Transform goal;

	public ArrayList spheres;
	public SwarmIntelligence swarm;

	void FixedUpdate(){

	}

	void Flock(){

	}
}


