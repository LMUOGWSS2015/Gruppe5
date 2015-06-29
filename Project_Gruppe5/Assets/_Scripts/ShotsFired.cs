using UnityEngine;
using System.Collections;

public class ShotsFired : MonoBehaviour {

	public AudioClip shotSound;
	public GameObject bullet;
	private float rightTrigger;
	int counter = 0;
	public int shotSpeed = 15;
	private GameObject[] bulletExplosion;
	private GameObject[] enemyExplosion;

	private PlayerMovement playerMovement;
	Animator animator;

	void Start(){
		animator = GameObject.Find ("roboBuddy").GetComponent<Animator>();
		playerMovement = GameObject.Find ("Player").GetComponent<PlayerMovement>();
		
	}

	void FixedUpdate(){

		if (playerMovement.playWithControllerMac) {

			rightTrigger = Input.GetAxis ("XboxMacRightTrigger");
			counter++;
		}
		else if(playerMovement.playWithControllerWin){

			rightTrigger = Input.GetAxis ("XboxWinRightTrigger");
			counter++;

		}

		if (rightTrigger < 0){
			if (!animator.GetNextAnimatorStateInfo(0).IsName("shooting"))
				animator.SetBool ("shooting", true);
			else 
				animator.SetBool ("shooting", false);

			animator.SetBool ("firing", true);
			if (counter > shotSpeed) {
				counter = 0;
				shoot ();
				}
		}else{
			animator.SetBool ("firing", false);
		}
	


	}
	
	void Update () {


		if (Input.GetKeyDown (KeyCode.Space)) {

			Instantiate (bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);

			bulletExplosion = GameObject.FindGameObjectsWithTag("bulletExplosion");
			enemyExplosion = GameObject.FindGameObjectsWithTag("enemyExplosion");
			foreach (GameObject obj in bulletExplosion) {
				Destroy(obj);
			}
			foreach (GameObject obj in enemyExplosion) {
				Destroy(obj);
			}
		}
	}

	void shoot(){
		this.GetComponent<AudioSource> ().PlayOneShot(shotSound);
		Instantiate (bullet, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
	}
}
