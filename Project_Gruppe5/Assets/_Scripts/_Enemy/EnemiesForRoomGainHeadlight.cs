using UnityEngine;
using System.Collections.Generic;

public class EnemiesForRoomGainHeadlight : Enemies {


	public Light AmbientLight;
	public Light SpotLight1;
	public Light SpotLight2;

	private AudioSource male;
	private AudioSource female;


	[System.Serializable]
	public struct EnemiesLeft {
		public int enemiesLeftForOpen;
		public DoorOpen[] doors;
		public GameObject[] enemiesToActivate;
	}
	
	public List<EnemiesLeft> enemiesLeft;

	private Transform headlight;

	void Awake(){

		SpotLight1.enabled = false;
		SpotLight2.enabled = false;

		male = this.GetComponentsInChildren<AudioSource> () [1];
		female = this.GetComponentsInChildren<AudioSource> () [2];

		headlight = GameObject.FindGameObjectWithTag ("HeadlightPickUp").transform;
		headlight.GetComponent<BoxCollider>().enabled = false;

		foreach (EnemiesLeft el in enemiesLeft) {
			foreach (GameObject e in el.enemiesToActivate) 
				ChangeActivated (e, false);
		}
	}

	public override void More(int amnt){
		base.More (amnt);
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

		if (size <= 0) {

			if(PlayerPrefs.GetString("gender").Equals("male")){
				male.Play();
			}
			else
				female.Play();

			AmbientLight.enabled = false;
			headlight.GetComponent<BoxCollider>().enabled = true;
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

