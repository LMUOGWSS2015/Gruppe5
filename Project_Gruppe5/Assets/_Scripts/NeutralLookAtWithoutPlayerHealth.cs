using UnityEngine;
using System.Collections;

public class NeutralLookAtWithoutPlayerHealth : MonoBehaviour {
	public float rotSpeed = 5f;
	private Transform player;
	
	
	protected void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	protected void Update(){
		Vector3 dir = player.position - transform.position;
		dir.y = 0;
		
		if(dir != Vector3.zero)
			StartCoroutine(Rotation(Quaternion.LookRotation(dir), rotSpeed));
	}
	
	IEnumerator Rotation(Quaternion to, float time) {
		float elapsedTime = 0f;
		while (elapsedTime < time) {
			elapsedTime += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(transform.rotation, to, elapsedTime);
			yield return new WaitForEndOfFrame ();
		}
		yield return null;
	}
	
}
