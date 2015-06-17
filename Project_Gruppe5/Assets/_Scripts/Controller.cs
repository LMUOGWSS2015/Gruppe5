using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	public GameObject playerObject;
	public float stepSpeed;
	private bool movePlayer = false;
	private Vector3 targetPos = new Vector3 (0f, 0.55f, -6f);
	private float step;
	private DoubleDoorsOpen doors;
	
	void Start () {
		doors = GameObject.FindGameObjectWithTag ("DDoorsLower").gameObject.GetComponent<DoubleDoorsOpen> ();
		Debug.Log (doors);
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
		
		if (Input.GetKeyDown (KeyCode.G)) {
			doors.enabled = true;
			movePlayer = true;
		}
		if (movePlayer) {
			step = stepSpeed * Time.time;
//			Debug.Log(step);
			playerObject.transform.position = Vector3.MoveTowards (playerObject.transform.position, targetPos, 0.1f);
			if (playerObject.transform.position == targetPos) {
				movePlayer = false;
				doors.closeDoors();
			}
		}
	}
}
