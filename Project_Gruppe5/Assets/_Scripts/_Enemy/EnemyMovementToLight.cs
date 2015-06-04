using UnityEngine;
using System.Collections;

public class EnemyMovementToLight : EnemyMovement {
	public float distance = 8f;

	protected override void Move(){
		if(Vector3.Distance(player.position, transform.position) <= distance)
			nav.SetDestination(gazeLight.position);
	}
}
