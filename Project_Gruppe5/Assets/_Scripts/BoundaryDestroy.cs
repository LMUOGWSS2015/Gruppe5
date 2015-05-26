using UnityEngine;
using System.Collections;

public class BoundaryDestroy : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	}
}
