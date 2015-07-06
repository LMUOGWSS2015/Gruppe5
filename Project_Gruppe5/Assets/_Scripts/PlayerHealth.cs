using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 3;                            	// The amount of health the player starts the game with.
	public int currentHealth = 3;			                    // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	//public AudioClip deathClip;                               // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	
	
	Animator anim;                                              // Reference to the Animator component.
	//AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	//ShotsFired shotsFired;     			                         // Reference to the PlayerShooting script.
	bool isDead;
	bool deathTriggered;   // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.

	public GameObject playerHit;
	public GameObject playerExplosion;

	public float explDuration = 1f;
	public float hitDuration = 2f;

	static int deadState = Animator.StringToHash("Base.Dead");  

	
	ShowDeathScreen sds;
	
	void Awake (){
		anim = GetComponentInChildren <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		//shotsFired = GetComponent <ShotsFired> ();

		currentHealth = startingHealth;

		sds = GameObject.FindGameObjectWithTag ("DeathScreen").GetComponentInChildren<ShowDeathScreen>();
		sds.Show(false);

		healthSlider.maxValue = startingHealth;
		healthSlider.value = startingHealth;
	}
	
	
	void Update () {
		if(damaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		
		// Reset the damaged flag.
		damaged = false;


		//Check if Player is In Dead State
		AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
		if (currentState.IsName("Base.Dead"))
		{

			//Explode :)
			Destroy(Instantiate (playerExplosion, this.gameObject.transform.position, Quaternion.identity),explDuration);
			Destroy (this.gameObject);
			sds.Show(true);
			isDead = true;
		}
	}
	
	
	public void TakeDamage (int amount) {

		GameObject.FindGameObjectWithTag ("MainCamera").gameObject.GetComponent<ScreenShake>().shake++;
		damaged = true;

		if (!isDead) {
			currentHealth -= amount;
			healthSlider.value = currentHealth;
		}

		//playerAudio.Play ();

		if(currentHealth <= 0 && !isDead) {
			isDead = true;
			Death ();
		} else {
			Destroy(Instantiate (playerHit, this.gameObject.transform.position, Quaternion.identity),hitDuration);
		}
	}
	
	
	void Death (){
		if(!deathTriggered)
			anim.SetTrigger ("Die");
		deathTriggered = true;
		
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();
		
		playerMovement.enabled = false;
	} 
}