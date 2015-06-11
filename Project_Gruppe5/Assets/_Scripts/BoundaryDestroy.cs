using UnityEngine;
using System.Collections;

public class BoundaryDestroy : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
//		Debug.Log ("exit: " + other.gameObject);
		Destroy(other.gameObject);
	}
}
