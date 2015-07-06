using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	Vector3 basePosition;
	public float shake;
	public float decreaseFactor = 3f;
	public float shakeAmount = 0.5f;
	
	// Use this for initialization
	void Start () {
		basePosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (shake > 0) {
			this.transform.position = new Vector3(basePosition.x+Random.Range(-shakeAmount, shakeAmount), basePosition.y+Random.Range(-shakeAmount, shakeAmount), basePosition.z+Random.Range(-shakeAmount,shakeAmount));
			Debug.Log (shake+","+(float)Time.deltaTime +","+decreaseFactor);
			shake -= ((float)Time.deltaTime) * (float)decreaseFactor;
			
		} else {
			shake = 0.0f;
			this.transform.position=basePosition;
		}
	}
}
