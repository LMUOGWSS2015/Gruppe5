using UnityEngine;
using System.Collections;

public class ChargeGlow : MonoBehaviour {

	private GameObject obj;
	private Renderer rend;
	public GameObject pipes;
	private Animator animator;
	public AudioClip activateSound;
	public GameObject activatableObject;
	private ChargeChecker checker;

	public GameObject pointLight;

	private float rStart = 28f;
	private float gStart = 38f;
	private float bStart = 28f;
	private float aStart = 255f;

	private float rEnd = 61f;
	private float gEnd = 158f;
	private float bEnd = 61f;
	private float aEnd = 255f;

	public float stepTime = 0.05f;

	private float rStep;
	private float gStep;
	private float bStep;
	private float aStep;

	private float r;
	private float g;
	private float b;
	private float a;

	bool doCharge = true;
	bool doCounterCharge = true;
	bool full = false;

	public int number = 1;

	public bool partOfOrder = true;
	public bool isBlocked = false;

	void Start () {
		obj = this.gameObject.transform.Find ("Sphere").gameObject;
		if(partOfOrder)
		checker = GameObject.FindGameObjectWithTag ("ChargeChecker").gameObject.GetComponent<ChargeChecker>();
		animator = GetComponent<Animator> ();
		rend = obj.GetComponent<Renderer> ();

		r = rStart;
		g = gStart;
		b = bStart;
		a = aStart;

		rStep = (rEnd - rStart) / 100f;
		gStep = (gEnd - gStart) / 100f;
		bStep = (bEnd - bStart) / 100f;
		aStep = (aEnd - aStart) / 100f;

		Shader shader = Shader.Find("Self-Illumin/Specular");
		rend.material.shader = shader;

		if(pipes!=null)
		for (int i = 0; i < pipes.transform.childCount; ++i) {
			pipes.transform.GetChild (i).GetComponent<Renderer> ().material.shader = shader;
		}
	}
	void EndAction(){
//		enabled = false;
		if (partOfOrder) {

			checker.ChargerCharged(number);
		} else  {
			activatableObject.GetComponent<Activatable>().enabled = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Light") {

			doCharge = true;
			doCounterCharge = false;

			StartCoroutine (Charging ());
			StopCoroutine(CounterCharge());
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Light") {
			doCounterCharge = true;
			doCharge = false;

			StartCoroutine (CounterCharge ());
			StopCoroutine (Charging ());
		}

		StartCoroutine (CounterCharge ());
		StopCoroutine (Charging ());
	}
	
	void Update () {

	}

	IEnumerator Charging () {
		/*
		Debug.Log (animator.GetCurrentAnimatorStateInfo (0).IsName ("Base.Opened"));
		while (!animator.GetCurrentAnimatorStateInfo(0).IsName ("Base.Opened")) {
			yield return new WaitForSeconds (stepTime);
		} */
		while (doCharge && !full) {
			//if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Closed"))
			//Debug.Log("Closed:"+animator.GetCurrentAnimatorStateInfo(0).IsName ("Base.Closed"));
			//Debug.Log("Open:"+animator.GetCurrentAnimatorStateInfo(0).IsName ("Base.Open"));
			//	continue;
	
			if (!isBlocked&&animator.GetCurrentAnimatorStateInfo(0).IsName ("Base.Opened")) {
			if (r < rEnd) {
				r += (rStep * 3);
				g += (gStep * 3);
				b += (bStep * 3);
				a += (aStep * 3);
			}
			else {
				this.GetComponent<AudioSource>().PlayOneShot(activateSound);
				animator.SetTrigger("close");
				doCharge = false;
				full = true;

				if(pointLight!=null) {
					if(!partOfOrder||checker.numChargers<3)
						pointLight.GetComponent<Light> ().color = Color.green;
					else
						pointLight.GetComponent<Light> ().color = Color.yellow;
				}
				EndAction();
			}

			rend.material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			if(pipes!=null)
			for (int i = 0; i < pipes.transform.childCount; ++i) {
				pipes.transform.GetChild (i).GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			}
			}
			yield return new WaitForSeconds (0.002f);
		}
	}

	IEnumerator CounterCharge () {
		while (doCounterCharge && !full) {
			if (r > rStart) {
				r -= (rStep * 3);
				g -= (gStep * 3);
				b -= (bStep * 3);
				a -= (aStep * 3);
			}
			else {
				doCounterCharge = false;
			}

			rend.material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			if(pipes!=null)
			for (int i = 0; i < pipes.transform.childCount; ++i) {
				pipes.transform.GetChild (i).GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			}
			yield return new WaitForSeconds (0.002f);
		}
	}

	public void resetCharge(){
		Debug.Log ("Reset");
		r = rStart;
		g = gStart;
		b = bStart;
		a = aStart;

		Color reset = new Color(r/255f, g/255f, b/255f, a/255f);
		rend.material.SetColor ("_Color", reset);
		if(pipes!=null)
		for (int i = 0; i < pipes.transform.childCount; ++i) {
			pipes.transform.GetChild (i).GetComponent<Renderer> ().material.SetColor ("_Color", reset);
		}
		
		//this.GetComponent<AudioSource>().PlayOneShot(activateSound);
		animator.SetTrigger ("open");

		if(pointLight!=null)
			pointLight.GetComponent<Light> ().color = Color.red;
		full = false;

		RaycastHit hit;
		Vector3 v = transform.position + new Vector3(0,5,0);
		if (Physics.Raycast (v, Vector3.down*5, out hit)) {
			if(hit.collider.gameObject.tag == "Light"){
				doCharge = true;
				StartCoroutine (Charging ());
			}
		}
	}
	public void setBlocked(bool blocked){
		isBlocked = blocked;
	}
}
