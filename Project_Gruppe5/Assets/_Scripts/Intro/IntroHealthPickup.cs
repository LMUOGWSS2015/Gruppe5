using UnityEngine;
using System.Collections;

public class IntroHealthPickup : MonoBehaviour {

	public Light doorlight;

	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player"){
			Destroy(this.gameObject);
			doorlight.color = Color.green;
		}
	}
}
