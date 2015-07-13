using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using iView;

public class MainMenu : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas controlsCanvas;
	public Button play;
	public Button controls;
	public Button exit;
	bool controllerMac = false;
	bool controllerWin = false;
	float updown;
	Button[] buttons = new Button[3];
	int current = 0;
	float startTime;
	float journeyLength;
	float speed = 2f;
	Vector3 start;
	Vector3 goal;
	bool moveCam = false;
	Camera cam;
	public GameObject player;
	public Animator anim;

	bool wait = false;
	float secsToWait = .3f;

	// Use this for initialization
	void Start () {
		goal = new Vector3 (0, 0.55f, -2);
		mainMenu = mainMenu.GetComponent<Canvas>();


		play = play.GetComponent<Button> ();
		controls = controls.GetComponent<Button> ();
		exit = exit.GetComponent<Button> ();
		
		buttons [0] = play;
		buttons [1] = controls;
		buttons [2] = exit;

		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) {
			controllerMac = true;
			controllerWin = false;
		} else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			controllerMac = false;
			controllerWin = true;
		}
	}

	public void exitPressed() {
		//Debug.Log ("exit pressed");
		Application.Quit ();
	}
	
	/*public void calibratePressed() {
		//calibrate Eye Tracker
		iView.SMIGazeController.Instance.StartCalibration (7);
	}*/

	public void controlsPressed() {
		controlsCanvas.GetComponent<CaPCanvas>().Show(true);
	}

	public void playPressed() {
		//Debug.Log ("play pressed");
		anim.SetTrigger("unbox");
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
				abutton = Input.GetButtonDown("Fire1");
			} else if (controllerWin) {
				updown = Input.GetAxis ("XboxWinLeftY");
				abutton = Input.GetButtonDown("Fire1");
			} else {
				updown = Input.GetAxis ("Vertical");
				
				//abutton = Input.GetButton("Space");
			}
			if(updown>0.2 && !wait) {
				current-=1;
				if (current<0)
					current = 0;
				else 
					StartCoroutine(Wait(secsToWait));
			}
			else if (updown<-0.2 && !wait) {
				current+=1;
				if(current>buttons.Length-1) current = buttons.Length-1;
				else
					StartCoroutine(Wait(secsToWait));
			}

			for (int i=0; i<buttons.Length; i++) {
				if(i==current)
					buttons[i].GetComponentInChildren<Text>().fontSize=24;
				else
					buttons[i].GetComponentInChildren<Text>().fontSize=20;
			}
			if(abutton) {
				//Debug.Log ("Active");
				switch(current){
				case 0: playPressed(); break;
				case 1: controlsPressed(); wait = false; break;
				case 2: exitPressed(); break;
				default: Debug.Log("Pressed non-existing button"); return;
				}
			}
		}
	}

	IEnumerator Wait(float secs){
		wait = true;
		yield return new WaitForSeconds(secs);
		wait = false;
	}
}
