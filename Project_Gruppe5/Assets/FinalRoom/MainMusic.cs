using UnityEngine;
using System.Collections;

public class MainMusic : MonoBehaviour {

	private AudioSource[] audioSource;

	public GameObject player;
	private PlayerHealth playerHealth;
	private bool firstDeathOfScene = false;

	void Start () {
		audioSource = this.gameObject.GetComponents<AudioSource> ();
		
		playerHealth = player.GetComponent <PlayerHealth> ();

		audioSource [0].enabled = true;

//		Debug.Log (audioSource [0].clip);
//		Debug.Log (audioSource [1].clip);
//		Debug.Log (playerHealth.isDead);
	}
	
	void Update () {
		if (playerHealth.isDead) {
			firstDeathOfScene = true;

			audioSource [0].Stop ();
			audioSource [0].enabled = false;
			
			audioSource [1].enabled = true;
		}
		
		if (!playerHealth.isDead && firstDeathOfScene) {
			audioSource [0].enabled = true;
			
			audioSource [1].Stop ();
			audioSource [1].enabled = false;
		}
	}
}
