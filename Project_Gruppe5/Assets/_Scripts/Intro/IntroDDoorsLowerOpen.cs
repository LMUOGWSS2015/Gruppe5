using UnityEngine;
using System.Collections;

public class IntroDDoorsLowerOpen : MonoBehaviour {
	public float doorSpeed = 7f;
	public float startGameDoorDist = 3f;
	
	private Transform leftDoor;
	private Transform rightDoor;

	private GameObject button;
	private GameObject crateLeft;
	private GameObject crateRight;

	private float leftClosed;
	private float rightClosed; 
	
	private Quaternion leftRotation;
	private Quaternion rightRotation; 
	
	private Quaternion leftRotationClose;
	private Quaternion rightRotationClose;
	
	//	private bool isLowerDoors = false;
	private bool closing = false;
	private bool firsttime = true;

	public GameObject manTV;
	public GameObject womanTV;
	private Renderer rendererMan;
	private Renderer rendererWoman;

	public Texture manTexture;
	public Texture womanTexture;
	public Texture blankTexture;

	public Light leftLight;
	public Light rightLight;

	private PlayerMovement player;

	AudioSource audioWelcome;
	AudioSource audioTVOn;
	AudioSource audioTVOff;

	private Vector3 buttonTargetPosition = new Vector3(0f, 0.57f, 6.52f);
	private Vector3 crateLeftTargetPosition = new Vector3(-13.27f, 0f, 0f);
	private Vector3 crateRightTargetPosition = new Vector3(13.27f, 0f, 0f);
	
	
	void Awake (){

		PlayerPrefs.SetString ("gender", null);

		leftLight.enabled = false;
		rightLight.enabled = false;
		

		audioWelcome = this.GetComponents<AudioSource> () [1];
		audioTVOn = this.GetComponents<AudioSource> () [0];

		rendererMan = manTV.GetComponent<Renderer> ();
		rendererWoman = womanTV.GetComponent<Renderer> ();
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		//		if (this.gameObject.tag == "DDoorsLower") {
		//			isLowerDoors = true;
		//		}
		
		//		if (!isLowerDoors) {
		leftDoor = GameObject.Find ("DoorLeftLower").transform;
		rightDoor = GameObject.Find ("DoorRightLower").transform;
		button = GameObject.Find ("Button");
		crateLeft = GameObject.Find ("CrateLeft");
		crateRight = GameObject.Find ("CrateRight");
		//		}
		//		else if (isLowerDoors) {
		//			leftDoor = GameObject.Find ("DoorLeft").transform;
		//			rightDoor = GameObject.Find ("DoorRight").transform;
		//		}
		
		leftRotation = Quaternion.AngleAxis (-70, Vector3.up);
		rightRotation = Quaternion.AngleAxis (70, Vector3.up);
		
	}
	
	public void closeDoors () {
		audioWelcome.Play();
		closing = true;
		leftRotationClose = Quaternion.AngleAxis(-0, Vector3.down);
		rightRotationClose = Quaternion.AngleAxis(0, Vector3.down);

		GameObject.Find ("Spotlight").GetComponent<CapsuleCollider> ().enabled = true;
	}

	public void playTVOn(){

		if (!audioWelcome.isPlaying && firsttime) {
			leftLight.enabled = true;
			rightLight.enabled = true;
			audioTVOn.Play ();
		}

		firsttime = false;

	}
	
	
	void Update () {

		if (!closing) {
			leftDoor.rotation = Quaternion.Slerp (leftDoor.rotation, leftRotation, .05f); 
			rightDoor.rotation = Quaternion.Slerp (rightDoor.rotation, rightRotation, .05f);
		}
		if (closing) {

			leftDoor.rotation = Quaternion.Slerp (leftDoor.rotation, leftRotationClose, .05f); 
			rightDoor.rotation = Quaternion.Slerp (rightDoor.rotation, rightRotationClose, .05f);

		}
			
			if(Quaternion.Angle(leftDoor.rotation, leftRotationClose) < Mathf.Epsilon && !audioWelcome.isPlaying){

			rendererMan.material.SetTexture(0,manTexture);
			rendererWoman.material.SetTexture(0,womanTexture);
			playTVOn();

			}

		if (player.male) {

			rendererWoman.material.SetTexture(0,blankTexture);
			rightLight.enabled = false;
			GameObject.Find("IntroController").GetComponent<IntroLevel>().StartLevel(true);
			button.transform.position = Vector3.MoveTowards(button.transform.position, buttonTargetPosition, Time.deltaTime * 0.1f);
	
		}
		if (player.female) {
			rendererMan.material.SetTexture (0, blankTexture);
			leftLight.enabled = false;
			GameObject.Find ("IntroController").GetComponent<IntroLevel> ().StartLevel (true);
			button.transform.position = Vector3.MoveTowards(button.transform.position, buttonTargetPosition, Time.deltaTime * 0.1f);
		}

		if (button.GetComponent<IntroButtonPress> ().isactive) {

			crateRight.transform.position = Vector3.MoveTowards(crateRight.transform.position, crateRightTargetPosition, 0.05f);
			crateLeft.transform.position = Vector3.MoveTowards(crateLeft.transform.position, crateLeftTargetPosition, 0.05f);

		}
	}
	}

