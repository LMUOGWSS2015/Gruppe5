using UnityEngine;
using System.Collections;

public class EnemyMovementFromPlayer : EnemyMovement {
	public float distance = 8f;
	
	protected override void Move (){
		if (Vector3.Distance (player.position, transform.position) <= distance) {
			Vector3 trans = transform.position - player.position;
			trans.y = 0;
			transform.rotation = Quaternion.LookRotation (trans);
		
			Vector3 runTo = transform.position + transform.forward * 5;
		
			NavMeshHit hit;
			NavMesh.SamplePosition (runTo, out hit, 5, 1 << NavMesh.GetAreaFromName ("Walkable"));
		
			nav.SetDestination (hit.position);
		}
	} 
}
