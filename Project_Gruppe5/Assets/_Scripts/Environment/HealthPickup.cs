using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	protected PlayerHealth playerHealth;
	
	void Awake (){
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerHealth> ();
	}
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player"){
			playerHealth.currentHealth = playerHealth.startingHealth;
			playerHealth.healthSlider.value = playerHealth.currentHealth;
			Destroy(this.gameObject);
		}
	}
}