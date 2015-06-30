 using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Canvas mainMenu;
	public Button play;
	public Button exit;
	public bool controllerMac = false;
	public bool controllerWin = false;
	public float updown;
	Button[] buttons = new Button[2];
	int current = 0;
	float startTime;
	float journeyLength;
	float speed = 2f;
	Vector3 start;
	Vector3 goal;
	bool moveCam = false;
	Camera cam;
	public GameObject player;

	// Use this for initialization
	void Start () {
		goal = new Vector3 (0, 0.55f, -2);
		mainMenu = mainMenu.GetComponent<Canvas>();
		play = play.GetComponent<Button> ();
		exit = exit.GetComponent<Button> ();
		
		buttons [0] = play;
		buttons [1] = exit;
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
			cam.transform.position = Vector3.Lerp (start, goal, fracJourney);
			player.transform.rotation = Quaternion.Slerp (player.transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, 0f)), 0.05f);
			player.transform.position = Vector3.Lerp (player.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -1f), fracJourney);

		} else {
			bool abutton = false;
			if (controllerMac) {
				updown = Input.GetAxis ("XboxMacLeftY");
				abutton = Input.GetButton("Fire1");
			} else if (controllerWin) {
				updown = Input.GetAxis ("XboxWinLeftY");
				
				abutton = Input.GetButton("Fire1");

			} else {
				updown = Input.GetAxis ("Vertical");
				
				//abutton = Input.GetButton("Space");
			}
			if(updown>0.2) {
				current-=1;
				if (current<0)
					current = 0;
			}
			else if (updown<-0.2) {
				current+=1;
				if(current>1) current = 1;
			}
			for (int i=0; i<buttons.Length; i++) {
				if(i==current)
					buttons[i].GetComponentInChildren<Text>().fontSize=24;
				else
					buttons[i].GetComponentInChildren<Text>().fontSize=20;
			}
			if(abutton) {
				Debug.Log ("Active");
					if(current==0)
					playPressed();
				else if(current==1)
					exitPressed();
			}
		}
	}
}
