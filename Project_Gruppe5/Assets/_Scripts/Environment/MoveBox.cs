using UnityEngine;
using System.Collections;

public class MoveBox : MonoBehaviour {
	GameObject player;
	PlayerMovement pm;
	bool reach = false;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		pm = player.GetComponent<PlayerMovement> ();
	}

	void Update(){
		bool pressed1;

		if (!pm.mac && ! pm.win)
			pressed1 = Input.GetKeyDown (KeyCode.Space);
		else if (pm.win)
			pressed1 = Input.GetButton ("Fire2");
		else
			pressed1 = Input.GetButton ("Fire2");
		
		if(pressed1){
			transform.parent = null;
		}

		bool pressed0;
		if (!pm.mac && ! pm.win)
			pressed0 = Input.GetKeyDown (KeyCode.Space);
		else if (pm.win)
			pressed0 = Input.GetButton ("Fire1");
		else
			pressed0 = Input.GetButton ("Fire1");
		
		if(pressed0){
			transform.parent = player.transform;
		}
	}
	
	void OnTriggerEnter (Collider other){
			Debug.Log(other.gameObject);
		if (other.gameObject.tag == "Player")
			reach = true;
	}

	void OnTriggerLeave (Collider other){
		if (other.gameObject.tag == "Player")
			reach = false;
	}
}
