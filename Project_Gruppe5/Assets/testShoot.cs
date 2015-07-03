using UnityEngine;
using System.Collections;

public class testShoot : MonoBehaviour {

	private 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Shoot(){
		transform.parent.gameObject.BroadcastMessage("shoot");

	}
}
