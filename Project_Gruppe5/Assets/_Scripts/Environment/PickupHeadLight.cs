using UnityEngine;
using System.Collections;

public class PickupHeadLight : MonoBehaviour {
	public GameObject pickupCam;
	private DoubleDoorsOpen ddo;
	private GameObject[] gazeLight;
	private Transform headlight;
	private Transform headlightAnim;
	private GameObject mainCam;
	private Transform player;
	private Vector3 playerPos;

	void Awake(){
		ddo = GameObject.FindGameObjectWithTag ("DDoors").gameObject.GetComponent<DoubleDoorsOpen> ();
		gazeLight = GameObject.FindGameObjectsWithTag ("Light");
		headlightAnim = transform.Find("HeadlightAnim");
		headlight = transform.Find ("headlight");
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		this.enabled = false;
	}
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player"){
			Destroy(headlight.gameObject);

			mainCam.SetActive(false);
			pickupCam.SetActive(true);
			headlightAnim.gameObject.SetActive(true);

			playerPos = player.position;
			player.position = new Vector3 (0,-10,0);
			player.GetComponent<PlayerMovement> ().enabled = false;

			this.enabled = true;
		}
	}

	void Update(){
		if (headlightAnim.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).IsName("Finished")){
			this.enabled = false;
			
			mainCam.SetActive(true);
			pickupCam.SetActive(false);
			Destroy (headlightAnim.gameObject);

			player.position = playerPos;
			player.GetComponent<PlayerMovement> ().enabled = true;
			
			ddo.enabled = true;

			foreach(GameObject gl in gazeLight)
				gl.GetComponent<Light> ().enabled = true;
		}
	}
}
