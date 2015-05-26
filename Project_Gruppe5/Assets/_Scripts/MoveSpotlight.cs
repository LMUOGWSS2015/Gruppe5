using UnityEngine;
using System.Collections;

public class MoveSpotlight : MonoBehaviour
{
	Vector3 newPosition;

	void Start () {
		newPosition = transform.position;
	}

	void Update() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			newPosition = new Vector3(hit.point.x, 5f, hit.point.z);
			transform.position = newPosition;
		}
	}
}