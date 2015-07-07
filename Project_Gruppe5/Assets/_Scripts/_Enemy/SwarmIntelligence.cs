using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmIntelligence : MonoBehaviour {
	public int amntOfSpheres = 20;
	public float spawnArea = 3.5f;
	
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

			Vector2 pos = new Vector2(transform.position.x, transform.position.z) + Random.insideUnitCircle * spawnArea;
			s.transform.position = new Vector3(pos.x, transform.position.y, pos.y);
			//s.transform.parent = transform;
			
			spheres.Add(s);
			s.transform.parent = transform;
		}
		GameObject.FindGameObjectWithTag ("DDoors").GetComponent<Enemies> ().More(amntOfSpheres);
	}

	void OnDrawGizmosSelected(){
		Gizmos.DrawSphere (transform.position, spawnArea);
	}
}