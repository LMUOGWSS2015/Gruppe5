using UnityEngine;
using System.Collections.Generic;

public class EnemiesForRoomGainHeadlight : Enemies {
	[System.Serializable]
	public struct EnemiesLeft {
		public int enemiesLeftForOpen;
		public DoorOpen[] doors;
		public GameObject[] enemiesToActivate;
	}
	
	public List<EnemiesLeft> enemiesLeft;

	void Awake(){
		foreach (EnemiesLeft el in enemiesLeft) {
			foreach (GameObject e in el.enemiesToActivate) 
				ChangeActivated (e, false);
		}
	}

	public override void Less(int amnt){
		base.Less (amnt);

		for(int i = enemiesLeft.Count-1; i >= 0; i--){
			if(size>enemiesLeft[i].enemiesLeftForOpen)
				return;
			if(size == enemiesLeft[i].enemiesLeftForOpen){
				foreach(DoorOpen door in enemiesLeft[i].doors)
					door.Open(true);
				foreach(GameObject e in enemiesLeft[i].enemiesToActivate)
					ChangeActivated(e, true);

				enemiesLeft.RemoveAt(i);
			}
		}
	}

	void ChangeActivated(GameObject e, bool active){
		if(e != null){
			EnemyMovement em = e.GetComponent<EnemyMovement>();

			if(em != null)
				em.enabled = active;

			/*EnemyAttackFar eaf = e.GetComponent<EnemyAttackFar>();
			if(eaf != null)
				eaf.enabled = active;
			else{
				EnemyAttackNear ean = e.GetComponent<EnemyAttackNear>();
				if(ean != null)
					ean.enabled = active;
			}*/

		}
	}
}

