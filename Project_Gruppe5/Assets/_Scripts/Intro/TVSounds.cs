using UnityEngine;
using System.Collections;

public class TVSounds : MonoBehaviour {

	private AudioSource audioOn;
	private AudioSource audioOff;

	private Light light;

	// Use this for initialization
	void Start () {
		
		audioOn = this.GetComponents<AudioSource> () [0];
		audioOff = this.GetComponents<AudioSource> () [1];
		light = this.GetComponent<Light> ();
		
		
	}

	void Awake(){

		audioOn.Play ();
	}


	// Update is called once per frame
	void Update () {

		if (light.isActiveAndEnabled) {
		
			audioOff.Play ();
		
		}
	
	}
}
