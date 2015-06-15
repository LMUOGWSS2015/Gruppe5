using UnityEngine;
using System.Collections;

public class ChargeGlow : MonoBehaviour {

	private GameObject obj;
	private Renderer rend;
	public GameObject pipe1;
	public GameObject pipe2;
	public GameObject pipe3;

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

	bool inEnter = false;
	bool counterCharge = true;

	public int number = 1;

	public bool partOfOrder = true;

	void Start () {
		obj = this.gameObject;
		rend = obj.GetComponent<Renderer> ();
//		rend.material.shader = Shader.Find("Self-Illumin/Specular");
//		rend.material.SetColor ("_Color", new Color(61f/255f, 158f/255f, 61f/255f, 255f/255f));

		r = rStart;
		g = gStart;
		b = bStart;
		a = aStart;

		rStep = (rEnd - rStart) / 100f;
		gStep = (gEnd - gStart) / 100f;
		bStep = (bEnd - bStart) / 100f;
		aStep = (aEnd - aStart) / 100f;
	}

	void EndAction(){
		inEnter = false;
		counterCharge = false;
		enabled = false;
		if (partOfOrder) {
			ChargeChecker checker = GameObject.FindGameObjectWithTag ("ChargeChecker").gameObject.GetComponent<ChargeChecker>();
			checker.ChargerCharged(number);
		} else  {
			DoubleDoorsOpen doors = GameObject.FindGameObjectWithTag ("DDoors").gameObject.GetComponent<DoubleDoorsOpen> ();
			//doors.OpenDoors ();
			doors.enabled = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Light") {
			Debug.Log ("enter");
			inEnter = true;
			counterCharge = false;
			StopCoroutine(CounterCharge());
			StartCoroutine (Charging ());
		}
	}

	void OnTriggerExit () {
		inEnter = false;
		counterCharge = true;
		if (!counterCharge) {
			StopCoroutine (Charging ());
			StartCoroutine (CounterCharge ());
		}
	}

	void Update () {
		if (!inEnter) {
			StopCoroutine (Charging ());
//			StartCoroutine (CounterCharge ());
		}
	}

	IEnumerator Charging () {
		while (inEnter) {
			if (r == rEnd)
				break;

			Debug.Log ("charging");

			if (r < rEnd) {
				r += rStep;
				g += gStep;
				b += bStep;
				a += aStep;
			}

			else {
				Debug.Log ("FULL");
				inEnter = false;
				counterCharge = false;
				StopCoroutine (CounterCharge());
				StopCoroutine (Charging());
//				EndAction();
			}
			rend.material.shader = Shader.Find("Self-Illumin/Specular");
			rend.material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe1.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
			pipe1.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe2.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
			pipe2.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe3.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
			pipe3.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			yield return new WaitForSeconds (stepTime);
		}
	}

	IEnumerator CounterCharge () {
		while (counterCharge) {
//			inEnter = true;
			if (r == rStart)
				break;

			Debug.Log ("countercharging");
			
			if (r > rStart) {
				r -= rStep;
				g -= gStep;
				b -= bStep;
				a -= aStep;
			}
			rend.material.shader = Shader.Find("Self-Illumin/Specular");
			rend.material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe1.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
			pipe1.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe2.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
			pipe2.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			pipe3.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Specular");
			pipe3.GetComponent<Renderer> ().material.SetColor ("_Color", new Color(r/255f, g/255f, b/255f, a/255f));
			yield return new WaitForSeconds (stepTime);
		}
	}
}
