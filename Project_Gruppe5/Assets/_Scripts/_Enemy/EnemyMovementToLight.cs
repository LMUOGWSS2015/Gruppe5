using UnityEngine;
using System.Collections;

public class EnemyMovementToLight : EnemyMovement {

	protected override void Move(){
		nav.SetDestination(gazeLight.position);
	}
}
