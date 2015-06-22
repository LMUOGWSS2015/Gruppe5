using UnityEngine;
using System.Collections;

public class EnemyMovementFromPlayer : EnemyMovement {
	
	protected override void Move (){
		if (frozen)
			nav.SetDestination (transform.position);
		else if (Vector3.Distance (player.position, transform.position) <= distance) {
			Vector3 trans = transform.position - player.position;
			trans.y = 0;
			Transform t = transform;
			t.rotation = Quaternion.LookRotation (trans);
			
			Vector3 runTo = t.position + t.forward * 5;
		
			NavMeshHit hit;
			NavMesh.SamplePosition (runTo, out hit, 5, 1 << NavMesh.GetAreaFromName ("Walkable"));
		
			nav.SetDestination (hit.position);
		}
	} 
}
