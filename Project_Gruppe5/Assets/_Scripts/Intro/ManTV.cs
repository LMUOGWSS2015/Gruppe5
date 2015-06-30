using UnityEngine;
using System.Collections;

public class ManTV : MonoBehaviour {
	
	Animator animator;
	AudioSource welcomeSource;
	AudioSource otherTVSound;
	AudioSource audio;

	private PlayerMovement player;
	
	void Start () {
		animator = this.GetComponent<Animator>();
		audio = GetComponent<AudioSource> ();
		welcomeSource = GameObject.Find("DDoorsLower").GetComponent<AudioSource>();
		otherTVSound = GameObject.Find ("TVset_woman").GetComponent<AudioSource> ();
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
	}
	
	
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Light") {
			
			if (!welcomeSource.isPlaying && !otherTVSound.isPlaying && !player.female && !player.male) {
				
				player.male = true;
				audio.Play ();
			}
		}
	}
	}

