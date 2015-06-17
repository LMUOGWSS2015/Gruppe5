using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {

	public GameObject bulletExplosion;
	public float explDuration = 2f;

	protected ArrayList tags = new ArrayList();

	protected virtual void Awake(){
		tags.Add ("Bullet");
		tags.Add ("enemyBullet");
	}

	void OnTriggerEnter (Collider other) {
		if (tags.Contains(other.gameObject.tag)) {
			Destroy(Instantiate (bulletExplosion, other.gameObject.transform.position, Quaternion.identity),explDuration);
			Destroy(other.gameObject);
		}
	}
}
