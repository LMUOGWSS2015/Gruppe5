using UnityEngine;
using System.Collections;



public class ObjectCrate : MonoBehaviour {

	public Transform brokenCrate;

	void OnMouseDown(){
		Destroy(Instantiate (brokenCrate,transform.position,transform.rotation),2f);
		Destroy (this);

	}
}
