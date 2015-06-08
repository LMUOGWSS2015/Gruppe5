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
	void Start () {
		Vector3 diagonal= new Vector3(stagewidth,0,stageheight);
		diagonallength=Vector3.Magnitude(diagonal);
		newPosition = transform.position;
		plane = GameObject.Find ("Floor").GetComponent<Collider>();
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
	}
}