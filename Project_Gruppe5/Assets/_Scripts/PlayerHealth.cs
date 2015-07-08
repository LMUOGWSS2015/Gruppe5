using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 10;                            	// The amount of health the player starts the game with.
	public int currentHealth;			                    // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	//public AudioClip deathClip;                               // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	
	
	Animator anim;                                              // Reference to the Animator component.
	//AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	//ShotsFired shotsFired;     			                         // Reference to the PlayerShooting script.
	public bool isDead;
	bool deathTriggered;   // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.

	public GameObject playerHit;
	public GameObject playerExplosion;

	public float explDuration = 1f;
	public float hitDuration = 2f;

	private ScreenShake screenShake;
	static int deadState = Animator.StringToHash("Base.Dead");  

	
	ShowDeathScreen sds;
	
	void Awake (){
		screenShake = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.GetComponent<ScreenShake> ();
		anim = GetComponentInChildren <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		//shotsFired = GetComponent <ShotsFired> ();

		currentHealth = PlayerPrefs.GetInt("health");

		sds = GameObject.FindGameObjectWithTag ("DeathScreen").GetComponentInChildren<ShowDeathScreen>();
		sds.Show(false);

		healthSlider.maxValue = startingHealth;
		healthSlider.value = currentHealth;
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

		screenShake.shake++;
		damaged = true;

		if (!isDead) {
			currentHealth -= amount;
			PlayerPrefs.SetInt("health", currentHealth);
			healthSlider.value = currentHealth;
		}

		//playerAudio.Play ();

		if(currentHealth <= 0 && !isDead) {
			isDead = true;
			Death ();
			PlayerPrefs.SetInt("health", 10);
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