using UnityEngine;
using System.Collections;

public class BulletForce : MonoBehaviour {

	public int bulletForce = 200;

	void Start () {
		this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);
	}
}
