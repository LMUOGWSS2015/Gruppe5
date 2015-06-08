using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {
	public int damagePerShot = 1;
	EnemyHealth enemyHealth;
	public GameObject bulletExplosion;
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Instantiate (bulletExplosion, other.gameObject.transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			enemyHealth = this.GetComponent <EnemyHealth> ();
			if(enemyHealth != null){
				Debug.Log("Hit");
				enemyHealth.TakeDamage (damagePerShot);
			}
		}
	}
}
