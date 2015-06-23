using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 8;
	public int currentHealth = 8;
	public GameObject enemyExplosion;
	//public AudioClip deathClip;    
	public float explDuration = 5f; 
	
	
	Animator anim;              
	//AudioSource enemyAudio;     
	CapsuleCollider capsuleCollider;   
	bool isDead;     

	
	void Awake (){
		anim = GetComponentInChildren <Animator> ();
	//	enemyAudio = GetComponent <AudioSource> ();

		currentHealth = startingHealth;
	}
	
	
	public void TakeDamage (int amount){
		if(isDead)
			return;

		//enemyAudio.Play ();
		
		currentHealth -= amount;
		
		if(currentHealth <= 0){
			Death ();
		}
	}
	
	
	void Death () {
		isDead = true;

//		anim.SetTrigger ("Dead");
		
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		Destroy (Instantiate (enemyExplosion, this.gameObject.transform.position, Quaternion.identity), explDuration);
//		Debug.Log(this.gameObject.tag);
		if (this.gameObject.tag == "Enemy") {
			capsuleCollider = GetComponent <CapsuleCollider> ();
			capsuleCollider.isTrigger = true;

			Destroy (this.gameObject);
		} else {
			capsuleCollider = GetComponentInParent <CapsuleCollider> ();
			capsuleCollider.isTrigger = true;
			
			BoxCollider boxCollider = GetComponent <BoxCollider> ();
			boxCollider.isTrigger = true;

			Destroy (this.transform.parent.gameObject);
		}

		transform.GetComponentInParent<Enemies> ().Less(1);
	}
}