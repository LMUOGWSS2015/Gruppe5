using UnityEngine;
using System.Collections;

public class EnemyMovementToPlayer : EnemyMovement {
	
	protected override void Move(){
		nav.SetDestination(player.position);
	}
}
