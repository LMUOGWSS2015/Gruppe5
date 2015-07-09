using UnityEngine;
using System.Collections;

public class IntroButtonPress : MonoBehaviour {
	private enum Stay{Never, UntilNextPress, Forever};
	private Stay stay = Stay.Forever;
	public bool pressed = false;
	public bool isactive = false;
	public bool playerActivated = false;

	private PlayerMovement player;
	
	ArrayList gOsPressingButton = new ArrayList ();
	ArrayList ignore = new ArrayList();
	private Renderer ringMaterialRenderer;
	private bool changed = false;
	
	AudioSource audioMale;
	AudioSource audioFemale;
	AudioSource tvWomanSound;
	AudioSource tvManSound;
	AudioSource crateSound;
	
	void Start(){

		audioMale = this.GetComponentsInChildren<AudioSource> () [1];
		audioFemale = this.GetComponentsInChildren<AudioSource> () [2];
		crateSound = this.GetComponentsInChildren<AudioSource> () [0];
		tvWomanSound = GameObject.Find ("TVset_woman").GetComponents<AudioSource> () [1];
		tvManSound = GameObject.Find ("TVset_man").GetComponents<AudioSource> () [1];
		ignore.Add ("Light");
		ignore.Add ("Floor");
		ignore.Add ("Bullet");
		ignore.Add ("enemyBullet");
		ignore.Add ("bulletExplosion");
		ignore.Add ("playerExplosion");
		ignore.Add ("enemyExplosion");
		
		ringMaterialRenderer = GetComponentsInChildren <Renderer> ()[1];

		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		
		if (pressed) {
			
			int mask = LayerMask.GetMask ("Shootable");
			RaycastHit hit;
			Vector3 v = transform.position + new Vector3 (0, 5, 0);
			if (Physics.Raycast (v, Vector3.down * 5, out hit, 5f, mask)) {
				if (hit.collider.gameObject.tag != "Light")
					gOsPressingButton.Add (hit.collider.gameObject);
			}
		} else {
			ringMaterialRenderer.material.SetColor ("_Color", Color.red);
		}
	}
	
	void OnTriggerEnter (Collider other){
		if(!ignore.Contains(other.gameObject.tag) && !(stay == Stay.Forever && changed)){
			if((!playerActivated
			    || (playerActivated && other.gameObject.tag == "Player"))
			   && !gOsPressingButton.Contains(other.gameObject)){

				crateSound.Play();

				isactive = true;

				if(player.female && !tvWomanSound.isPlaying && !tvManSound.isPlaying){
					audioFemale.Play();	
				}
				if(player.male && !tvWomanSound.isPlaying && !tvManSound.isPlaying){
					audioMale.Play();
				}

				ringMaterialRenderer.material.SetColor ("_Color", Color.green);
				gOsPressingButton.Add(other.gameObject);
				if(stay == Stay.UntilNextPress && changed) {
					changed = false; 
				} else {
					changed = true; 
				}
			}
		}
	}
	
	void OnTriggerExit (Collider other){
		if (stay != Stay.Forever) {
			if (gOsPressingButton.Contains (other.gameObject)) {
				gOsPressingButton.Remove (other.gameObject);
				
				
				if (gOsPressingButton.Count == 0 && stay != Stay.UntilNextPress) {
					ringMaterialRenderer.material.SetColor ("_Color", Color.red);
					changed = false;
				}
			}
		}
	}
}

