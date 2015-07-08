using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowDeathScreen : MonoBehaviour {


	public Button restart;
	public Button mainmenu;
	Button[] buttons = new Button[2];
	int current = 0;
	public bool controllerMac = false;
	public bool controllerWin = false;
	private float updown;

//	private AudioSource gameOver;

//	private bool started = false;

	// Use this for initialization
	void Start(){
		Debug.Log ("Start");
		restart = restart.GetComponent<Button> ();
		mainmenu = mainmenu.GetComponent<Button> ();
		
		buttons [0] = restart;
		buttons [1] = mainmenu;

//		gameOver = this.gameObject.GetComponent<AudioSource> ();
//		Debug.Log (gameOver);

//		started = true;
//		Debug.Log (started);
	}
	public void Show (bool show) {
		Debug.Log ("show");
		current = 0;
		this.gameObject.SetActive (show);
//		if (started) {
//			gameOver.enabled = true;
//		}
	}

	public void ToMainMenu(){
//		gameOver.Stop ();
//		gameOver.enabled = false;
		Application.LoadLevel ("mainmenu");
	}

	public void RestartLevel(){
//		gameOver.Stop ();
//		gameOver.enabled = false;
		PlayerPrefs.SetInt ("health", 10);
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Update(){
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
				buttons[i].GetComponentInChildren<Text>().fontSize=15;
		}
		if(abutton) {
			if(current==0)
				RestartLevel();
			else if(current==1)
				ToMainMenu();
		}
	}
}
