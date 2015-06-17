using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Canvas mainMenu;
	public Button play;
	public Button exit;
	float startTime;
	float journeyLength;
	float speed = 2f;
	Vector3 start;
	Vector3 goal;
	bool moveCam = false;
	Camera cam;

	// Use this for initialization
	void Start () {
		goal = new Vector3 (0, 0.55f, -2);
		mainMenu = mainMenu.GetComponent<Canvas>();
		play = play.GetComponent<Button> ();
		exit = exit.GetComponent<Button> ();
	}

	public void exitPressed() {
		Debug.Log ("exit pressed");
		Application.Quit ();
	}
	public void playPressed() {
		Debug.Log ("play pressed");
		
		cam = Camera.main;
		start = cam.transform.position;
		startTime = Time.time;
		journeyLength = Vector3.Distance(start,goal);
		moveCam = true;
	}

	public void Update(){
		if (moveCam) {
			// Distance moved = time * speed.
			var distCovered = (Time.time - startTime) * speed;
			
			// Fraction of journey completed = current distance divided by total distance.
			var fracJourney = distCovered / journeyLength;
			cam.transform.position = Vector3.Lerp(start,goal,fracJourney);
			if(Vector3.Distance(cam.transform.position,goal)<0.1) {
				Application.LoadLevel ("_Room1Something");
			}
		}
	}
}
