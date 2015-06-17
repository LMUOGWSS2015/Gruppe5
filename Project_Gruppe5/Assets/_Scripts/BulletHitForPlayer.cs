using UnityEngine;
using System.Collections;

public class BulletHitForPlayer : MonoBehaviour {

	public int damagePerShot = 1;
	EnemyHealth playerHealth;
	public GameObject bulletExplosion;
	
	public float explDuration = 2f;
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "enemyBullet") {
			Destroy(Instantiate (bulletExplosion, other.gameObject.transform.position, Quaternion.identity),explDuration);
			Destroy(other.gameObject);
			playerHealth = this.GetComponent <EnemyHealth> ();
			if(playerHealth != null){
				playerHealth.TakeDamage (damagePerShot);
			}
		}
	}
}
