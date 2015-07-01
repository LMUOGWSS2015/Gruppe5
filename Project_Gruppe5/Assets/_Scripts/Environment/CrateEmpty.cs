using UnityEngine;
using System.Collections;

public class CrateEmpty : MonoBehaviour {
	
	
	public float explDuration = 2f;
	public GameObject brokenCrate;
	public GameObject blockedObject;
	
	protected ArrayList tags = new ArrayList();
	
	protected virtual void Awake(){
		tags.Add ("Bullet");
		tags.Add ("enemyBullet");
	}
	
	void OnTriggerEnter (Collider other) {
		if (tags.Contains(other.gameObject.tag)) {
			blockedObject.GetComponent<ChargeGlow>().setBlocked(false);
			Destroy(Instantiate (brokenCrate,transform.position,transform.rotation),1f);
			Destroy (this.gameObject);
		}
	}
}
