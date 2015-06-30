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

	// Use this for initialization
	void Start(){
		restart = restart.GetComponent<Button> ();
		mainmenu = mainmenu.GetComponent<Button> ();
		
		buttons [0] = restart;
		buttons [1] = mainmenu;
	}
	public void Show (bool show) {
		current = 0;
		this.gameObject.SetActive (show);
	}

	public void ToMainMenu(){
		Application.LoadLevel ("mainmenu");
	}

	public void RestartLevel(){
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
