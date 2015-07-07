using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	protected PlayerHealth playerHealth;
	public AudioClip pickupHealthSound;
	
	void Awake (){
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerHealth> ();
	}
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player"){
			this.GetComponent<AudioSource>().PlayOneShot(pickupHealthSound);
			this.GetComponent<BoxCollider>().enabled=false;
			Behaviour halo = (Behaviour)GetComponent("Halo");
			
			halo.enabled = false; // false
			playerHealth.currentHealth = playerHealth.startingHealth;
			playerHealth.healthSlider.value = playerHealth.currentHealth;
			
			Destroy(this.gameObject,0.5f);
		}
	}
}