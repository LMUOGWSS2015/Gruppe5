using UnityEngine;
using System.Collections;

public class EnterNewLevelSound : MonoBehaviour {

	public AudioClip audioMale;
	public AudioClip audioFemale;

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {

			if(PlayerPrefs.GetString("gender") == "male")
			this.GetComponent<AudioSource>().PlayOneShot(audioMale);
			else
				this.GetComponent<AudioSource>().PlayOneShot(audioFemale);

		}
	}
}
