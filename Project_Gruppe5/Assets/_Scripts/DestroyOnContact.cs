using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {

	public GameObject bulletExplosion;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Instantiate (bulletExplosion, other.gameObject.transform.position, Quaternion.identity);
			Destroy(other.gameObject);
		}
	}
}
