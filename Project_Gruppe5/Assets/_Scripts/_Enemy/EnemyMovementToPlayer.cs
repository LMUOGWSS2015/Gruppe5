using UnityEngine;
using System.Collections;

public class EnemyMovementToPlayer : EnemyMovement {
	
	protected override void Move(){
		if (frozen)
			nav.SetDestination (transform.position);
		else if(Vector3.Distance (player.position, transform.position) <= distance)
			nav.SetDestination(player.position);
	}
}
