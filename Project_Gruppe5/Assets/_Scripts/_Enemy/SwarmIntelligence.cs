using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmIntelligence : MonoBehaviour {
	public int amntOfSpheres = 20;
	public float spawnArea = 3f;
	
	public List<GameObject> spheres;
	public GameObject sphere;

	
	protected virtual void Start () {
		GameObject s;
		spheres = new List<GameObject> ();
		for (int i = 0; i < amntOfSpheres; i++) {
			s = Instantiate(sphere);
			SphereBehaviour sb = s.GetComponent<SphereBehaviour> ();
			sb.spheres = this.spheres;
			sb.swarm = this;

			Vector3 pos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * spawnArea;
			s.transform.position = new Vector3(pos.x, transform.position.y, pos.y);
			//s.transform.parent = transform;
			
			spheres.Add(s);
		}
	}
}