using UnityEngine;
using System.Collections;

public class ShowSparkle : MonoBehaviour {
	Animator animator;

	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Light"){
			Debug.Log("true");
			animator.SetBool ("Light", true);
		}
	}
	
	void OnTriggerExit (Collider other){
		if(other.gameObject.tag == "Light"){
			Debug.Log("false");
			animator.SetBool ("Light", false);
		}
	}
}
