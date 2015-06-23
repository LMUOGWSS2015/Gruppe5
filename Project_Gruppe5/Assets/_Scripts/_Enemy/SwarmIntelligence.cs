using UnityEngine;
using System.Collections;

public class SwarmIntelligence : MonoBehaviour {
	public int amntOfSpheres = 20;
	public float spawnArea = 10f;
	public Vector2 swarmBounds = new Vector2(10f, 10f);
	
	public ArrayList spheres;
	public GameObject sphere;

	
	protected virtual void Start () {
		GameObject s;
		spheres = new ArrayList();
		for (int i = 0; i < amntOfSpheres; i++) {
			s = Instantiate(sphere);
			SphereBehaviour sb = s.GetComponent<SphereBehaviour> ();
			sb.spheres = this.spheres;
			sb.swarm = this;

			Vector2 pos = new Vector2(transform.position.x, transform.position.z) + Random.insideUnitCircle * spawnArea;
			s.transform.position = new Vector3(pos.x, transform.position.y, pos.y);
			s.transform.parent = transform;
			
			spheres.Add(s);
		}
	}