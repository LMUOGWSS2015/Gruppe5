using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {
	public int damagePerShot;
	EnemyHealth enemyHealth;
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Destroy(other.gameObject);
			enemyHealth = this.GetComponent <EnemyHealth> ();
			if(enemyHealth != null){
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (damagePerShot);
			}
		}
	}
}
