using UnityEngine;
using System.Collections;

public class ObjectShatter : MonoBehaviour {
	

	public float explDuration = 2f;
	public GameObject brokenCrate;
	
	protected ArrayList tags = new ArrayList();
	
	protected virtual void Awake(){
		tags.Add ("Bullet");
		tags.Add ("enemyBullet");
	}
	
	void OnTriggerEnter (Collider other) {
		if (tags.Contains(other.gameObject.tag)) {
			Destroy(Instantiate (brokenCrate,transform.position,transform.rotation),5f);
			Destroy (this.gameObject);
		}
	}
}
