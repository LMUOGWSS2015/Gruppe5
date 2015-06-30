using UnityEngine;
using System.Collections;

public class WomanTV : MonoBehaviour {

	Animator animator;
	
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Light"){

			Debug.Log("Looking at Woman");

			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
			audio.Play(44100);
		}
	}
	
	void OnTriggerExit (Collider other){
		if(other.gameObject.tag == "Light"){

			Debug.Log("Away");

		}
	}
}
