﻿using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {
	public AudioClip slideSound = null;
	public float doorSpeed = 7f;
	public bool isOpen = false;

	private bool toOpen = false;
	private bool toClose = false;
	private float startTime;
	private float journeyLength;
	private Vector3 up;
	private Vector3 down;

	private BoxCollider boxCollider;

	void Start(){
		boxCollider = GetComponent<BoxCollider> ();
		if(isOpen){
			up = transform.position + Vector3.up * transform.localScale.y;
			down = transform.position;
			boxCollider.isTrigger = true;
		} else {
			up = transform.position;
			down = transform.position - Vector3.up * transform.localScale.y;
		}
		journeyLength = Vector3.Distance(up, down);
	}

	void FixedUpdate () {
		if (toOpen) {
			var distCovered = (Time.time - startTime) * doorSpeed;
		
			var fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp (up, down, fracJourney);
			if(Vector3.Distance(transform.position, down) < Mathf.Epsilon){
				boxCollider.isTrigger = true;
				toOpen = false;
			}
		} else if(toClose) {
			var distCovered = (Time.time - startTime) * doorSpeed;
			
			var fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp (down, up, fracJourney);
			if(Vector3.Distance(transform.position, up) < Mathf.Epsilon)
				toClose = false;
		}
	}

	public void Open(bool openDoor){
		this.GetComponent<AudioSource>().PlayOneShot(slideSound);
		if (openDoor) {
			toOpen = true;
			toClose = false;
		} else {
			boxCollider.isTrigger = false;
			toClose = true;
			toOpen = false;
		}
		startTime = Time.time;
	}
}
