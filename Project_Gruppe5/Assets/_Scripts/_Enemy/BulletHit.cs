using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {
	public int damagePerShot = 1;
	EnemyHealth enemyHealth;
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Destroy(other.gameObject);
			enemyHealth = this.GetComponent <EnemyHealth> ();
			if(enemyHealth != null){
				enemyHealth.TakeDamage (damagePerShot);
			}
		}
	}
}
