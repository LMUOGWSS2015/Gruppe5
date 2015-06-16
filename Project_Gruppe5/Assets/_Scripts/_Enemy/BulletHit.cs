using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {
	public int damagePerShot = 1;
	EnemyHealth enemyHealth;
	public GameObject bulletExplosion;

	public float explDuration = 2f;
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Destroy(Instantiate (bulletExplosion, other.gameObject.transform.position, Quaternion.identity),explDuration);
			Destroy(other.gameObject);
			enemyHealth = this.GetComponent <EnemyHealth> ();
			if(enemyHealth != null){
				enemyHealth.TakeDamage (damagePerShot);
			}
		}
	}
}
