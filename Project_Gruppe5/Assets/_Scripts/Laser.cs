using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public int dmgAmnt = 1;
	public float range = 20f;
	
	LineRenderer gunLine; 
	Ray shootRay;  
	RaycastHit shootHit;
	GameObject player;
	PlayerHealth playerHealth;
	int mask;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.gameObject.GetComponent<PlayerHealth> ();

		gunLine = GetComponentInChildren <LineRenderer> ();
		gunLine.enabled = true;
		
		mask = LayerMask.GetMask ("Shootable");
	}
	
	// Update is called once per frame
	void Update () {
		gunLine.SetPosition (0, gunLine.transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;
//		Debug.DrawRay (shootRay.origin, shootRay.direction, Color.white, 100, false);
		if (Physics.Raycast (shootRay, out shootHit, range, mask)) {
			gunLine.SetPosition (1, shootHit.point);

			if (shootHit.transform.gameObject == player) {
				playerHealth.TakeDamage (dmgAmnt);
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Light"){
			gunLine.enabled = false;
			this.enabled = false;
			//animator.SetBool("frozen",frozen);
		}
	}
	
	void OnTriggerExit (Collider other){
		if(other.gameObject.tag == "Light"){
			gunLine.enabled = true;
			this.enabled = true;
			//animator.SetBool("frozen",frozen);
		}
	}
}
