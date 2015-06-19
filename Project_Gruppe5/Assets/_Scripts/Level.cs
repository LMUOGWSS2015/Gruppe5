using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	GameObject player;
	GameObject[] enemies;

	PlayerMovement pm;
	EnemyMovement[] em;
	//EnemyAttack
	ShotsFired sf;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		pm = player.GetComponent<PlayerMovement> ();
		sf = player.GetComponentInChildren<ShotsFired> ();

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	
	public void StartLevel(bool start){
		pm.enabled = start;
		sf.enabled = start;

		foreach (GameObject e in enemies) {
			e.GetComponent<EnemyMovement>().enabled = start;
			/*EnemyAttackFar eaf = e.GetComponent<EnemyAttackFar>();
			if(eaf != null)
				eaf.enabled = false;
			else{*/
				EnemyAttackNear ean = e.GetComponent<EnemyAttackNear>();
				if(ean != null)
					ean.enabled = start;
			//}
		}
	}
}
