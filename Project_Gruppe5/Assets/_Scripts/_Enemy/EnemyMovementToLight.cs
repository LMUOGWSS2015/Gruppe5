using UnityEngine;
using System.Collections;

public class EnemyMovementToLight : EnemyMovement {

	protected override void Move(){
		if (frozen)
			nav.SetDestination (transform.position);
		else if (Vector3.Distance (gazeLight.position, transform.position) <= distance)
			nav.SetDestination(gazeLight.position);
	}
}
