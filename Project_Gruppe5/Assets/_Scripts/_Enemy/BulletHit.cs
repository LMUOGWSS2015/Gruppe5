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
			if (this.gameObject.tag == "Enemy")
				this.GetComponent<FlashRed>().Flash();
			else
				this.transform.parent.GetComponent<FlashRed>().Flash();
			enemyHealth = this.GetComponent <EnemyHealth> ();
			if(enemyHealth != null){
				enemyHealth.TakeDamage (damagePerShot);
			}
		}
	}


}
