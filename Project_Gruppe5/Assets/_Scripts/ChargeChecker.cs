using UnityEngine;
using System.Collections;

public class ChargeChecker : MonoBehaviour {

	public int numChargers = 2;
	int[] checkArray;
	// Use this for initialization
	void Start () {
		clearArray ();
	}
	public void ChargerCharged(int num){
		for (int i=0; i<checkArray.Length; i++) {
			if (checkArray [i] == 0) {
				checkArray[i]=num;
				break;
			}
		}
		if (isFull()) {
			if(isInOrder()) {
				DoubleDoorsOpen doors = GameObject.FindGameObjectWithTag ("DDoors").gameObject.GetComponent<DoubleDoorsOpen> ();
				//doors.OpenDoors ();
				doors.enabled = true;
			} else {
				resetChargers();
				clearArray();
			}
		}
	}
	void resetChargers(){
		GameObject[] chargers = GameObject.FindGameObjectsWithTag("Charger");
		foreach (GameObject charger in chargers) {
			ChargeGlow c = charger.gameObject.GetComponent<ChargeGlow>();
			c.resetCharge();
		}
	}

	//clear array
	void clearArray(){
		checkArray = new int[numChargers];
	}

	//check if the array is full, that there are no 0's
	bool isFull() {
		for (int i=0; i<checkArray.Length; i++) {
			if (checkArray [i] == 0)
				return false;
		}
		return true;
	}
	bool isInOrder(){
		int currentval = 0;
		for (int i=0; i<checkArray.Length; i++) {
			if(checkArray[i]==0)
				return false;
			if(checkArray[i]>currentval) {
				currentval=checkArray[i];
			}else return false;
		}
		return true;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
