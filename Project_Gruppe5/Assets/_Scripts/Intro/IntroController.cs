using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {
	
	public GameObject playerObject;
	public float stepSpeed;
	public Canvas pauseCanvas;
	public GameObject doubleDoorsLower;

	private bool movePlayer = false;
	private Vector3 targetPos = new Vector3 (0f, 0.55f, -6f);
	private float step;
	private IntroDDoorsLowerOpen doors;
	private PlayerMovement pm;
	
	IntroLevel level;
	
	void Start () {
		//		doors = GameObject.FindGameObjectWithTag ("DDoorsLower").gameObject.GetComponent<DoubleDoorsLowerOpen> ();
		doors = doubleDoorsLower.gameObject.GetComponent<IntroDDoorsLowerOpen> ();
		//		Debug.Log (doors);
		
		level = transform.gameObject.GetComponent<IntroLevel> ();
		level.StartLevel (false);
		
		doors.enabled = true;
		movePlayer = true;

		PlayerPrefs.SetInt ("health", 10);
		
		pm = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		} else if ((pm.win && Input.GetButtonDown ("Start"))
			|| (pm.mac && Input.GetButtonDown ("StartMac"))) {
			if (!pauseCanvas.GetComponent<PauseCanvas> ().GetPause ())
				pauseCanvas.GetComponent<PauseCanvas> ().Show (true);
		}
		
		//		if (Input.GetKeyDown (KeyCode.G)) {
		//			doors.enabled = true;
		//			movePlayer = true;
		//		}
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
