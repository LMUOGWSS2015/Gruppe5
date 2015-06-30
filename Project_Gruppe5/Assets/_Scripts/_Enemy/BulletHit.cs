using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {
	public int damagePerShot = 1;
	EnemyHealth enemyHealth;
	public GameObject bulletExplosion;
	
	private Material redMaterial;
	private ArrayList originalMaterials = new ArrayList();
	private int indexForOldMaterial = 0;

	public float explDuration = 2f;
	public float redFlashTime = 0.15f;

	void Start() {
		redMaterial = (Resources.Load ("Red_Flash", typeof(Material))) as Material;
		getOriginalMaterials (this.transform);
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Destroy(Instantiate (bulletExplosion, other.gameObject.transform.position, Quaternion.identity),explDuration);
			Destroy(other.gameObject);
			enemyHealth = this.GetComponent <EnemyHealth> ();
			StartCoroutine(FlashRed());
			if(enemyHealth != null){
				enemyHealth.TakeDamage (damagePerShot);
			}
		}
	}

	IEnumerator FlashRed() {
		setRedMaterial (this.transform);
		yield return new WaitForSeconds (redFlashTime);
		indexForOldMaterial = 0;
		setOldMaterials(this.transform);
	}

	
	private void getOriginalMaterials(Transform t) {
		int i = 0;
		foreach (Transform child in t){
			getOriginalMaterials(child);
			i++;
		}
		
		if (i == 0){
			Renderer rend = t.GetComponent<Renderer>();
			if(rend!=null) {
			Material mat = rend.material;
			originalMaterials.Add(mat);
			}
		}
	}

	private void setRedMaterial(Transform t) {
		int i = 0;
		foreach (Transform child in t){
			setRedMaterial(child);
			i++;
		}
		
		if (i == 0){
			Renderer rend = t.GetComponent<Renderer>();
			if(rend!=null) {
			rend.material=redMaterial;
			}
		}
	}

	private void setOldMaterials(Transform t) {
		int i = 0;
		foreach (Transform child in t){
			setOldMaterials(child);
			i++;
		}
		
		if (i == 0){
			Renderer rend = t.GetComponent<Renderer>();
			if(rend!=null) {
				rend.material=(Material)originalMaterials[indexForOldMaterial];
				indexForOldMaterial++;
			}
		}
	}
}
