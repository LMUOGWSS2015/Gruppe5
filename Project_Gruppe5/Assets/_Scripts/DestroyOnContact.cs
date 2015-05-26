using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Destroy(other.gameObject);
		}
	}
}
