using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	protected PlayerHealth playerHealth;
	public AudioClip pickupHealthSound;
	private bool toPick = false;
	//Only for IntroRoom
	public Light spotlight;
	
	void Awake (){
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerHealth> ();
	}
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player" && toPick){
			this.GetComponent<AudioSource>().PlayOneShot(pickupHealthSound);
			this.GetComponent<BoxCollider>().enabled=false;
			Behaviour halo = (Behaviour)GetComponent("Halo");
			
			halo.enabled = false; // false

			if(!(playerHealth == null)){
			playerHealth.currentHealth = playerHealth.startingHealth;
			playerHealth.healthSlider.value = playerHealth.currentHealth;
			}
			else
				spotlight.color = Color.green;
			
			Destroy(this.gameObject,0.5f);
		}
	}
	
	public void SetToPick (bool b){
		toPick = b;
	}
}