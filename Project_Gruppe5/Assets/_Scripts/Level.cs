using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	GameObject player;
	GameObject[] enemies;

	PlayerMovement pm;
	EnemyMovement[] em;
	ShotsFired sf;

	bool[] activated;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		pm = player.GetComponent<PlayerMovement> ();
		sf = player.GetComponentInChildren<ShotsFired> ();

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		activated = new bool[enemies.Length];
	}
	
	public void StartLevel(bool start){
		pm.enabled = start;
		sf.enabled = start;

		for(int i = 0; i<enemies.Length; i++){
		//foreach (GameObject e in enemies) {
			if(enemies[i] != null){
				EnemyMovement em = enemies[i].GetComponent<EnemyMovement>();
				if(em != null){
					if(!start){
						activated [i] = em.enabled;
						em.enabled = start;
					} else {
						em.enabled = activated[i];
					}
				}

				EnemyAttackFar eaf = enemies[i].GetComponent<EnemyAttackFar>();
				if(eaf != null)
					eaf.enabled = start;
				else{
					EnemyAttackNear ean = enemies[i].GetComponent<EnemyAttackNear>();
					if(ean != null)
						ean.enabled = start;
				}
			}
		}
	}
}
