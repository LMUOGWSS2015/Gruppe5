using UnityEngine;
using System.Collections;

public class ShotsFired : MonoBehaviour {

	public GameObject bullet;

	void Start () {
	
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1.1f), Quaternion.identity);
		}
	}
}
