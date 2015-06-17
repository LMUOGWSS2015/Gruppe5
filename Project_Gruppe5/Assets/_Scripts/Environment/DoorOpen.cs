using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {
	public float doorSpeed = 7f;

	private bool open = false;
	private float startTime;
	private float journeyLength;
	private Vector3 up;
	private Vector3 down;

	void Start(){
		up = transform.position;
		down = transform.position - Vector3.up * transform.localScale.y;
		journeyLength = Vector3.Distance(up, down);
	}

	void Update () {
		if (open) {
			var distCovered = (Time.time - startTime) * doorSpeed;
		
			var fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp (up, down, fracJourney);
		} else {
			var distCovered = (Time.time - startTime) * doorSpeed;
			
			var fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp (down, up, fracJourney);
		}
	}

	public void Open(){
		open = true;
		startTime = Time.time;
	}

	public void Close(){
		open = false;
		startTime = Time.time;
	}
}
