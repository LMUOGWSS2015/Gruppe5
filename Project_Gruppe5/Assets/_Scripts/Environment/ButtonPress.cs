using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
	public DoorOpen door;

	bool pressed = false;
	ArrayList al = new ArrayList ();

	void Start(){
		if (pressed) {
			door.Open ();

			RaycastHit hit;
			Vector3 v = transform.position + new Vector3 (0, 5, 0);
			if (Physics.Raycast (v, Vector3.down * 5, out hit)) {
				if(hit.collider.gameObject.tag != "Light")
					al.Add(hit.collider.gameObject);
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag != "Light" && other.gameObject.tag != "Floor"){
			pressed = true;
			al.Add(other.gameObject);
			door.Open();
		}
	}
	
	void OnTriggerExit (Collider other){
		if (al.Contains(other.gameObject)) {
			al.Remove(other.gameObject);
		}

		if(al.Count == 0){
			pressed = false;
			door.Close ();
		}
	}
}
