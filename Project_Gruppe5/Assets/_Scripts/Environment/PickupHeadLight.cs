using UnityEngine;
using System.Collections;

public class PickupHeadLight : MonoBehaviour {
	DoubleDoorsOpen ddo;

	void Awake(){
		ddo = GameObject.FindGameObjectWithTag ("DDoors").gameObject.GetComponent<DoubleDoorsOpen> ();
	}
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player"){
			ddo.enabled = true;
			Destroy(this.gameObject);
		}
	}
}
