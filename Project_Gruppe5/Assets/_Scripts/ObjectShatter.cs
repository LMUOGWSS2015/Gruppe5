using UnityEngine;
using System.Collections;

public class ObjectShatter : MonoBehaviour {
	public float explDuration = 2f;
	public GameObject brokenCrate;
	
	protected ArrayList tags = new ArrayList();
	HealthPickup hp;
	
	protected virtual void Awake(){
		tags.Add ("Bullet");
		tags.Add ("enemyBullet");

		hp = this.transform.parent.GetComponentInChildren<HealthPickup> ();
	}
	
	void OnTriggerEnter (Collider other) {
		if (tags.Contains(other.gameObject.tag)) {
			hp.SetToPick(true);
			Destroy (Instantiate (brokenCrate,transform.position,transform.rotation), 3f);
			Destroy (this.gameObject);
		}
	}
}
