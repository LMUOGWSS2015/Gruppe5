using UnityEngine;
using System.Collections;

public class BulletForce : MonoBehaviour {

	public int bulletForce = 100;

	void Start () {
		this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);
	}
}
