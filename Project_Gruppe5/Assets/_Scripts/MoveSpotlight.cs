using UnityEngine;
using System.Collections;
using iView;

public class MoveSpotlight : MonoBehaviour
{
	public bool useEyeTracking = false;
	public float speed = 2f;
	public float stagewidth = 40f;
	public float stageheight = 20f;
	private Collider plane;
	Vector3 newPosition;
	Vector3 dPosition;
	SampleData sample;
	Vector3 averageGazePosition;
	float diagonallength;
	private Light lt1;
	private Light lt2;
	private Light lt3;
	private bool done = false;

	private AudioSource source;

	private float random;

	public bool flicker = true;


	void Start () {
		Vector3 diagonal= new Vector3(stagewidth,0,stageheight);
		diagonallength=Vector3.Magnitude(diagonal);
		newPosition = transform.position;
		plane = GameObject.Find ("Floor").GetComponent<Collider>();

		lt1 = this.GetComponent<Light>();
		lt2 = this.GetComponentsInChildren<Light> () [1];
		lt3 = this.GetComponentsInChildren<Light> () [2];

		source = this.GetComponent<AudioSource>();
		source.mute = true;
	}
	
	void Update() {

		Ray ray;
		if (useEyeTracking) {
			//get the Sample from the Server
			sample = SMIGazeController.Instance.GetSample ();
			//get the averaged GazePosition
			averageGazePosition = sample.averagedEye.gazePosInUnityScreenCoords ();
		
			if (averageGazePosition.x == 0f) 
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			else
				ray = Camera.main.ScreenPointToRay (averageGazePosition);
		} else
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;
		if (plane.Raycast(ray, out hit,100f)) {
			newPosition = new Vector3(hit.point.x, 5f, hit.point.z);
			Vector3 to = newPosition-transform.position;
			float length= Vector3.Magnitude(to);
			to = to.normalized;
			transform.position = transform.position+(to*speed*length/diagonallength);
		}



		if (Input.GetKeyDown (KeyCode.T)) {
			StartCoroutine (LightFlicker ());
		}
		if (done) {
			flicker = true;
			StopCoroutine (LightFlicker ());
			Debug.Log ("FLICKER STOPPED");
		}

		if (flicker) {
			random = Random.value;
			Debug.Log (random);
			if (random > 0.999f) {
				StartCoroutine (LightFlicker ());
				flicker = false;
			}
		}
	}

	IEnumerator LightFlicker () {
		Debug.Log ("IN FLICKER");
		ChangeIntensityTo (3f);
		yield return new WaitForSeconds (0.5f);

		ChangeIntensityTo (6f);
		yield return new WaitForSeconds (0.3f);

		source.mute = !source.mute;

		ChangeIntensityTo (8f);
		yield return new WaitForSeconds (0.2f);

		ChangeIntensityTo (4f);
		yield return new WaitForSeconds (0.3f);

		ChangeIntensityTo (0f);
		yield return new WaitForSeconds (0.15f);

		ChangeIntensityTo (4f);
		yield return new WaitForSeconds (0.15f);

		ChangeIntensityTo (5f);
		yield return new WaitForSeconds (0.15f);

		ChangeIntensityTo (6f);
		yield return new WaitForSeconds (0.15f);

		source.mute = !source.mute;

		ChangeIntensityTo (7f);
		yield return new WaitForSeconds (0.15f);
		done = true;

		ChangeIntensityTo (8f);

		done = true;
	}

	void ChangeIntensityTo (float intensity) {
		lt1.intensity = intensity;
		lt2.intensity = intensity;
		lt3.intensity = intensity;
	}
}