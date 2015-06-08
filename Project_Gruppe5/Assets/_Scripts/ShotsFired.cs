using UnityEngine;
using System.Collections;

public class ShotsFired : MonoBehaviour {

	public GameObject bullet;
	private float rightTrigger;
	int counter = 0;
	public int shotSpeed = 15;
	private GameObject[] bulletExplosion;
	private GameObject[] enemyExplosion;

	void Start () {
	
	}

	void FixedUpdate(){

		rightTrigger = Input.GetAxis ("XboxMacRightTrigger");
		counter++;
		if (rightTrigger < 0 && counter > shotSpeed) {
			counter = 0;
			Instantiate (bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
		
		}


	}
	
	void Update () {


		if (Input.GetKeyDown (KeyCode.Space)) {

			Instantiate (bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1.1f), Quaternion.identity);

			bulletExplosion = GameObject.FindGameObjectsWithTag("bulletExplosion");
			enemyExplosion = GameObject.FindGameObjectsWithTag("enemyExplosion");
			foreach (GameObject obj in bulletExplosion) {
				Destroy(obj);
			}
			foreach (GameObject obj in enemyExplosion) {
				Destroy(obj);
			}
		}
	}
}
