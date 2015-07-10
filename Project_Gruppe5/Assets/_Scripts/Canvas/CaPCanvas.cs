using UnityEngine;
using System.Collections;

public class CaPCanvas : MonoBehaviour {
	bool exit;
	
	public virtual void Show (bool show) {
		this.gameObject.SetActive (show);
		this.enabled = show;
	}
	
	void Update(){
		exit = Input.GetButtonDown ("Fire1") 
			|| Input.GetButtonDown ("Fire2")
			|| Input.GetButtonDown ("Fire3")
			|| Input.GetButtonDown ("Jump")
			|| Input.GetButtonDown ("Start")
			|| Input.GetButtonDown ("StartMac");
		
		if (exit)
			Show (false);
	}
}
