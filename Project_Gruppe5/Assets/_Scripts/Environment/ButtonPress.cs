using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
	public DoorOpen door;
	public enum Stay{Never, UntilNextPress, Forever};
	public Stay stay = Stay.Never;
	public bool opensDoor = true;
	public bool pressed = false;
	public bool playerActivated = false;

	ArrayList gOsPressingButton = new ArrayList ();
	ArrayList ignore = new ArrayList();

	private Renderer ringMaterialRenderer;

	private bool changed = false;

	void Start(){
		ignore.Add ("Light");
		ignore.Add ("Floor");
		ignore.Add ("Bullet");
		ignore.Add ("enemyBullet");
		ignore.Add ("bulletExplosion");
		ignore.Add ("playerExplosion");
		ignore.Add ("enemyExplosion");

		ringMaterialRenderer = GetComponentsInChildren <Renderer> ()[1];

		if (pressed) {
			door.Open (opensDoor);

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
			if(!gOsPressingButton.Contains(other.gameObject)){
				ringMaterialRenderer.material.SetColor ("_Color", Color.green);
				gOsPressingButton.Add(other.gameObject);
				if(stay == Stay.UntilNextPress && changed) {
					door.Open(!opensDoor);
					changed = false; 
				} else {
					door.Open(opensDoor);
					changed = true; 
				}
			} else if(playerActivated)
				return;
		}
	}
	
	void OnTriggerExit (Collider other){
		if (stay != Stay.Forever) {
			if (gOsPressingButton.Contains (other.gameObject)) {
				gOsPressingButton.Remove (other.gameObject);


				if (gOsPressingButton.Count == 0 && stay != Stay.UntilNextPress) {
					ringMaterialRenderer.material.SetColor ("_Color", Color.red);
					door.Open (!opensDoor);
					changed = false;
				}
			}
		}
	}
}
