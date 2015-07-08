using UnityEngine;
using System.Collections;

public class ManTV : MonoBehaviour {
	
	Animator animator;
	AudioSource welcomeSource;
	AudioSource otherTVSound;
	AudioSource audio;
	AudioSource tvOff;

	private PlayerMovement player;
	
	void Start () {
		animator = this.GetComponent<Animator>();
		audio = this.GetComponents<AudioSource> () [1];
		tvOff = this.GetComponents<AudioSource> () [0];
		welcomeSource = GameObject.Find("DDoorsLower").GetComponents<AudioSource>()  [0];
		otherTVSound = GameObject.Find ("TVset_woman").GetComponent<AudioSource> ();
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
	}
	
	
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Light") {
			
			if (!welcomeSource.isPlaying && !otherTVSound.isPlaying && !player.female && !player.male) {
				
				player.male = true;
				PlayerPrefs.SetString("gender", "male");
				tvOff.Play ();
				audio.Play ();
			}
		}
	}
	}

