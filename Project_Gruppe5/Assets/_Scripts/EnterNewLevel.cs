using UnityEngine;
using System.Collections;

public class EnterNewLevel : MonoBehaviour {

	IEnumerator enterNewLevel(){


		float fadeTime = this.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel("_RoomMaze");


	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {

//			Debug.Log("Loading Level 2");
			StartCoroutine(enterNewLevel());

		}
	}
}
