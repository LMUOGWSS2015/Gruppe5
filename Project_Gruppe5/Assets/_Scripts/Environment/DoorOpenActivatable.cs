using UnityEngine;
using System.Collections;

public class DoorOpenActivatable : Activatable {


	private DoorOpen[] doors;
	private bool open = false;

	
	
	void Awake (){

	}
	
	
	void Update () {
		if (!open) {
			for (int i=0; i<this.transform.childCount; i++) {
				this.transform.GetChild (i).gameObject.GetComponent<DoorOpen> ().Open (true);
			}
			open=true;
		}

	}

}