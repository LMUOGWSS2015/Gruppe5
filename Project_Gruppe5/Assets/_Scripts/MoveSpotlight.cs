using UnityEngine;
using System.Collections;
using iView;

public class MoveSpotlight : MonoBehaviour
{
	Vector3 newPosition;
	Vector3 dPosition;
	SampleData sample;
	Vector3 averageGazePosition;
	public float speed = 2f;
	public float stagewidth = 40f;
	public float stageheight = 20f;
	float diagonallength;
	void Start () {
		Vector3 diagonal= new Vector3(stagewidth,0,stageheight);
		diagonallength=Vector3.Magnitude(diagonal);
		newPosition = transform.position;
	}

	void Update() {
		
		//get the Sample from the Server
		sample = SMIGazeController.Instance.GetSample();
		//get the averaged GazePosition
		averageGazePosition = sample.averagedEye.gazePosInUnityScreenCoords();

		Debug.Log ("agp:"+averageGazePosition+" m:"+ Input.mousePosition);
		Ray ray;

		if (averageGazePosition.x==0f) 
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		 else
			ray = Camera.main.ScreenPointToRay (averageGazePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			newPosition = new Vector3(hit.point.x, 5f, hit.point.z);
			Vector3 to = newPosition-transform.position;
			float length= Vector3.Magnitude(to);
			to = to.normalized;
			transform.position = transform.position+(to*speed*length/diagonallength);
		}
	}
}