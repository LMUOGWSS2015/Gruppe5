using UnityEngine;
using System.Collections;

public class EnemyMovementToPlayer : EnemyMovement {
	public float distance = 8f;
	
	protected override void Move(){
		if(Vector3.Distance(player.position, transform.position) <= distance)
			nav.SetDestination(player.position);
	}
}
