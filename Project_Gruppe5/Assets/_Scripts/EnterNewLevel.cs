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

			Debug.Log("Loading Level: " + Levelname);
			StartCoroutine(enterNewLevel());

		}
	}
}
