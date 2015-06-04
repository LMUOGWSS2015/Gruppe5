using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour {
	public float timeToChargeInSec = 3f;
	public float chargePercentage = 0;

	bool charging;
	float timer;

	void Update () {
		timer += Time.deltaTime;
		
		if (timer >= 0.3f && chargePercentage < 100) {
			timer = 0;
		}

		if (chargePercentage >= 100) {
			charging = false;
		}
		
		if(charging) {
			chargePercentage += Time.deltaTime * (100 / timeToChargeInSec);
		}
		
		chargePercentage = Mathf.Clamp(chargePercentage, 0, 100);

		float c = chargePercentage/100f;
		this.gameObject.GetComponent<Renderer>().material.color =  new Color(c,c,c, 1f);
	}

	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Light"){
			charging = true;
		}
	}
	
	void OnTriggerExit (Collider other){
		if(other.gameObject.tag == "Light"){
			charging = false;
		}
	}
}
