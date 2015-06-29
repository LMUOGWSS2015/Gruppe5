using UnityEngine;
using System.Collections;

public class SetPlayerRotaForStartScreen : MonoBehaviour {

	public GameObject player;

	void Start () {
		player.transform.rotation = Quaternion.Euler (new Vector3 (0f, 180f, 0f));
	}

	// Update is called once per frame
	void Update () {
//		player.transform.rotation = Quaternion.Euler (new Vector3 (0f, 180f, 0f));
	}
}
