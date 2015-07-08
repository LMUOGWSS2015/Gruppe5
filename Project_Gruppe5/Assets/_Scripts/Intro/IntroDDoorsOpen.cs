using UnityEngine;
using System.Collections;

public class IntroDDoorsOpen : Activatable {
	public float doorSpeed = 7f;
	public bool roomHasEnemies = true;
	
	private bool enemiesDead;
	private bool opened = false;
	private bool nextLevel = false;
	
	private Transform leftDoor;
	private Transform rightDoor;
	private float leftClosed;
	private float rightClosed; 
	
	private Quaternion leftRotation;
	private Quaternion rightRotation;

	public Light rightlight;
	public Light leftlight;

	public AudioClip doorOpenSound;

	private AudioSource maleAudio;
	private AudioSource femaleAudio;
	
	
	void Awake (){
		leftDoor = GameObject.Find ("DoorLeft").transform;
		rightDoor = GameObject.Find ("DoorRight").transform;
		
		leftRotation = Quaternion.AngleAxis(-70, Vector3.up);
		rightRotation = Quaternion.AngleAxis(70, Vector3.up);

		maleAudio = this.GetComponents<AudioSource>() [1];
		femaleAudio = this.GetComponents<AudioSource>() [2];

	}
	
	
	void Update () {

		if (rightlight.color == Color.green && leftlight.color == Color.green) {

			if(!nextLevel){

				if(PlayerPrefs.GetString("gender").Equals("male")){
					maleAudio.Play();
				}
				else
					femaleAudio.Play ();

				nextLevel = true;
			}

			if(!opened && nextLevel && (!femaleAudio.isPlaying && !maleAudio.isPlaying)){

			this.GetComponent<AudioSource>().PlayOneShot(doorOpenSound);

				opened = true;
			}

			if (opened){

			leftDoor.rotation = Quaternion.Slerp (leftDoor.rotation, leftRotation, .05f); 
			rightDoor.rotation = Quaternion.Slerp (rightDoor.rotation, rightRotation, .05f);

			}
		}
	}
	
	public void SetEnemiesDead(bool ed){
		enemiesDead = ed;
	}
	
	public bool GetEnemiesDead(){
		return enemiesDead;
	}
}