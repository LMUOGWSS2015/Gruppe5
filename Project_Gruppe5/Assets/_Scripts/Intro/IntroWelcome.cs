using UnityEngine;
using System.Collections;

public class IntroWelcome : MonoBehaviour {

	Animator animator;
	
	void Start () {
		animator = this.GetComponent<Animator>();
	}

	
	void OnTriggerExit (Collider other){
		if(other.gameObject.tag == "Player"){
			
				AudioSource audio = GetComponent<AudioSource>();
				audio.Play();
				audio.Play(44100);
			}
			
		}
	}

