using UnityEngine;
using System.Collections;

public class ManTV : MonoBehaviour {
	
	Animator animator;
	AudioSource welcomeSource;
	AudioSource otherTVSound;
	AudioSource audio;
	
	void Start () {
		animator = this.GetComponent<Animator>();
		audio = GetComponent<AudioSource> ();
		welcomeSource = GameObject.Find("DDoorsLower").GetComponent<AudioSource>();
		otherTVSound = GameObject.Find ("TVset_woman").GetComponent<AudioSource> ();
	}
	
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Light"){
			
			if(!welcomeSource.isPlaying && !otherTVSound.isPlaying){
				Debug.Log("Looking at Man");	
				audio.Play();
			}
		}
		
	}
}
