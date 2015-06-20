using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
	public DoorOpen door;
	public bool opensDoor = true;
	public bool stay = false;
	public bool pressed = false;

	ArrayList gOsPressingButton = new ArrayList ();
	ArrayList ignore = new ArrayList();

	void Start(){
		ignore.Add ("Light");
		ignore.Add ("Floor");
		ignore.Add ("Bullet");
		ignore.Add ("enemyBullet");
		ignore.Add ("bulletExplosion");
		ignore.Add ("playerExplosion");
		ignore.Add ("enemyExplosion");

		if (pressed) {
			door.Open (opensDoor);

			int mask = LayerMask.GetMask ("Shootable");
			RaycastHit hit;
			Vector3 v = transform.position + new Vector3 (0, 5, 0);
			if (Physics.Raycast (v, Vector3.down * 5, out hit, 5f, mask)) {
				if(hit.collider.gameObject.tag != "Light")
					gOsPressingButton.Add(hit.collider.gameObject);
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if(!ignore.Contains(other.gameObject.tag)){
			gOsPressingButton.Add(other.gameObject);
			door.Open(opensDoor);
		}
	}
	
	void OnTriggerExit (Collider other){
		if (!stay) {
			if (gOsPressingButton.Contains (other.gameObject)) {
				gOsPressingButton.Remove (other.gameObject);

				if (gOsPressingButton.Count == 0) {
					door.Open (!opensDoor);
				}
			}
		}
	}
}
