using UnityEngine;
using System.Collections;

public class EnterNewLevel : MonoBehaviour {

	public string Levelname;

	IEnumerator enterNewLevel(){


		float fadeTime = this.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(Levelname);


	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {

			if(!Application.loadedLevelName.Equals("_IntroRoom") && !Application.loadedLevelName.Equals("mainmenu")){
			PlayerPrefs.SetInt("health", GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerHealth> ().currentHealth);
			}
			else
				PlayerPrefs.SetInt("health", 10);
				StartCoroutine(enterNewLevel());


		}
	}
}
