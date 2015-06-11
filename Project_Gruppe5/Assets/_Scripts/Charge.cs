using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour {
	public float timeToChargeInSec = 3f;
	public float chargePercentage = 0;
	public bool partOfOrder = true;
	public int number = 0;
	
	bool charging;
	float timer;

	Animator animator;

	void Start(){
		animator = this.GetComponentInChildren<Animator>();
	}
	
	void Update () {
		if (chargePercentage >= 100) {
			EndAction ();
			return;
		}

		if(charging) {
			chargePercentage += Time.deltaTime * (100 / timeToChargeInSec);
		}
		
		chargePercentage = Mathf.Clamp(chargePercentage, 0, 100);
		
		changeMaterial ();

	}

	void EndAction(){
		enabled = false;
		if (partOfOrder) {
			ChargeChecker checker = GameObject.FindGameObjectWithTag ("ChargeChecker").gameObject.GetComponent<ChargeChecker>();
			checker.ChargerCharged(number);
		} else  {
			DoubleDoorsOpen doors = GameObject.FindGameObjectWithTag ("DDoors").gameObject.GetComponent<DoubleDoorsOpen> ();
			//doors.OpenDoors ();
			doors.enabled = true;
		}
	}

	public void resetCharge(){
		chargePercentage = 0;
		changeMaterial ();
		this.enabled = true;
	}

	void changeMaterial(){
		if (chargePercentage == 0)
			animator.SetInteger ("Charge", 0);
		else if (chargePercentage < 34)
			animator.SetInteger ("Charge", 1);
		else if (chargePercentage < 67)
			animator.SetInteger ("Charge", 2);
		else if (chargePercentage < 100)
			animator.SetInteger ("Charge", 3);
		else
			animator.SetInteger ("Charge", 4);
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
