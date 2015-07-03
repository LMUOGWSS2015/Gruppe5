using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	GameObject player;
	GameObject[] enemies;

	PlayerMovement pm;
	EnemyMovement[] em;
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
			if(e != null){
				EnemyMovement em = e.GetComponent<EnemyMovement>();
				if(em != null)
					em.enabled = start;

				EnemyAttackFar eaf = e.GetComponent<EnemyAttackFar>();
				if(eaf != null)
					eaf.enabled = start;
				else{
					EnemyAttackNear ean = e.GetComponent<EnemyAttackNear>();
					if(ean != null)
						ean.enabled = start;
				}
			}
		}
	}
}
