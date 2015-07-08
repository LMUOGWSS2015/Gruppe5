using UnityEngine;
using System.Collections;

public class IntroHealthPickup : MonoBehaviour {

	public Light doorlight;
	public AudioClip pickupHealthSound;
	private bool toPick = false;
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player" && toPick){
			this.GetComponent<AudioSource>().PlayOneShot(pickupHealthSound);
			this.GetComponent<BoxCollider>().enabled=false;
			Behaviour halo = (Behaviour)GetComponent("Halo");
			
			halo.enabled = false; // false
			
			Destroy(this.gameObject,0.5f);
		}
	}
	
	public void SetToPick (bool b){
		toPick = b;
	}
}
