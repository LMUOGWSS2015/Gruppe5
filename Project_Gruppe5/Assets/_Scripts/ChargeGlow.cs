using UnityEngine;
using System.Collections;

public class ChargeGlow : MonoBehaviour {

	private GameObject obj;
	private Renderer rend;
	public GameObject pipe1;
	public GameObject pipe2;
	public GameObject pipe3;

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

	void Start () {
		obj = this.gameObject;
		rend = obj.GetComponent<Renderer> ();

		r = rStart;
		g = gStart;
		b = bStart;
		a = aStart;

		rStep = (rEnd - rStart) / 100f;
		gStep = (gEnd - gStart) / 100f;
		bStep = (bEnd - bStart) / 100f;
		aStep = (aEnd - aStart) / 100f;

		
		rend.material.shader = Shader.Find("Self-Illumin/Specular");
		pipe1.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
		pipe2.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
		pipe3.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
	}

	void EndAction(){
//		enabled = false;
		if (partOfOrder) {
			ChargeChecker checker = GameObject.FindGameObjectWithTag ("ChargeChecker").gameObject.GetComponent<ChargeChecker>();
			checker.ChargerCharged(number);
		} else  {
			DoubleDoorsOpen doors = GameObject.FindGameObjectWithTag ("DDoors").gameObject.GetComponent<DoubleDoorsOpen> ();
//			doors.OpenDoors ();
			doors.enabled = true;
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
		while (doCharge && !full) {
			if (r < rEnd) {
				r += rStep;
				g += gStep;
				b += bStep;
				a += aStep;
			}
			else {
				doCharge = false;
				full = true;
				pointLight.GetComponent<Light> ().color = Color.green;
				EndAction();
			}

			rend.material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe1.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe2.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe3.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));

			yield return new WaitForSeconds (stepTime);
		}
	}

	IEnumerator CounterCharge () {
		while (doCounterCharge && !full) {
			if (r > rStart) {
				r -= rStep;
				g -= gStep;
				b -= bStep;
				a -= aStep;
			}
			else {
				doCounterCharge = false;
			}

			rend.material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe1.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe2.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe3.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			yield return new WaitForSeconds (stepTime);
		}
	}

	public void resetCharge(){
		r = rStart;
		g = gStart;
		b = bStart;
		a = aStart;

		Color reset = new Color(r/255f, g/255f, b/255f, a/255f);
		rend.material.SetColor ("_Color", reset);
		pipe1.GetComponent<Renderer> ().material.SetColor ("_Color", reset);
		pipe2.GetComponent<Renderer> ().material.SetColor ("_Color", reset);
		pipe3.GetComponent<Renderer> ().material.SetColor ("_Color", reset);

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
}
