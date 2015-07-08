using UnityEngine;
using System.Collections;

public class FinalLevelController : MonoBehaviour {

	public GameObject camObj;
	private Camera cam;
	private Transform camTrans;
	private bool shaking = false;
	private float shake = 3f;
	private float shakeAmount = 0.7f;
	private float decreaseFactor = 1.0f;
	private Vector3 originalPos;

	public GameObject playerObj;
	private PlayerMovement playerMovement;

	public GameObject galaxyPortal;
	private GameObject galaxyPortalClone;
	private bool galaxyTwist = false;
	private bool galaxyGlow = false;

	private Light galaxyPortalCloneLight;

	private bool levelBeaten = false;

	public GameObject finalWinMenu;

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	private Vector3 origin = new Vector3 (0f, 0f, 0f);
	private GameObject enemy1CloneFirst;
	private GameObject enemy1Clone;
	private GameObject enemy2Clone;
	private int enemy1counter = 0;
	private Vector3 lerpEnemy1 = new Vector3 (0f, 0f, 0f);
	private float xValue = 17f;
	private float zValue = 13f;
	
	private bool doneWithEnemies = false;

	private AudioSource[] audioSource;

	public GameObject player;
	private PlayerHealth playerHealth;
	private int health;
	
	void Start () {
		cam = camObj.GetComponent<Camera> ();
		camTrans = camObj.transform;

		playerMovement = playerObj.GetComponent<PlayerMovement> ();

		audioSource = this.gameObject.GetComponents<AudioSource> ();

		playerHealth = player.GetComponent <PlayerHealth> ();
		Debug.Log (playerHealth.isDead);

		health = PlayerPrefs.GetInt ("health");
	}
	
	void Update () {
		if (shaking) {
			if (shake > 0f) {
				camTrans.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
				shake -= Time.deltaTime * decreaseFactor;

				galaxyGlow = true;

				galaxyPortalCloneLight.intensity += 0.017f;
			}
			else if (shake < 0f && shake != 0f){
				shake = 0f;
				camTrans.localPosition = originalPos;
				shaking = false;
				lockPlayerMovement (false);
				StartCoroutine (InfinityEnemies ());
				StartCoroutine (FadeOutGalaxyRising ());
			}
		}

		if (galaxyGlow && galaxyPortalClone.transform.localScale.x < 8.0f) {
			galaxyPortalClone.transform.localScale += new Vector3 (0.05f, 0f, 0.05f);
		}

		if (galaxyTwist) {
			galaxyPortalClone.transform.Rotate (0f, -30f * Time.deltaTime, 0f);
		}

		if (levelBeaten) {
			StopCoroutine (InfinityEnemies ());
			finalWinMenu.SetActive(true);
		}

		if (enemy1CloneFirst != null) {
			enemy1CloneFirst.transform.position = Vector3.Lerp (enemy1CloneFirst.transform.position, new Vector3 (origin.x, origin.y, 15f), Time.deltaTime * 1f);
			if (enemy1CloneFirst.transform.position.z > 14.5f) {
				enemy1CloneFirst = null;
			}
		}
		if (enemy1Clone != null) {
			if (enemy1counter % 4 == 1) {
				lerpEnemy1.x = xValue;
				lerpEnemy1.z = zValue;
			}
			else if (enemy1counter % 4 == 2) {
				lerpEnemy1.x = -xValue;
				lerpEnemy1.z = zValue;
			}
			else if (enemy1counter % 4 == 3) {
				lerpEnemy1.x = xValue;
				lerpEnemy1.z = -zValue;
			}
			else if (enemy1counter % 4 == 0) {
				lerpEnemy1.x = -xValue;
				lerpEnemy1.z = -zValue;
			}
			enemy1Clone.transform.position = Vector3.Lerp (enemy1Clone.transform.position, lerpEnemy1, Time.deltaTime * 1f);
			if (enemy1Clone.transform.position.x > xValue - 0.5f) {
				enemy1Clone = null;
			}
		}

		if ((GameObject.FindGameObjectWithTag ("Enemy") == null) && doneWithEnemies) {
			levelBeaten = true;
			audioSource [2].enabled = false;
			audioSource [3].enabled = false;
		}

		health = PlayerPrefs.GetInt ("health");
		Debug.Log (health);
		if ((playerHealth.isDead)) {
			audioSource [2].Stop ();
			audioSource [2].enabled = false;
			audioSource [3].Stop ();
			audioSource [3].enabled = false;
		}
	}

	void shakeCam () {
		originalPos = camTrans.localPosition;
		shaking = true;
	}

	void lockPlayerMovement (bool value) {
		if (value) {
			playerMovement.enabled = false;
		} else if (!value) {
			playerMovement.enabled = true;
		}
	}

	void startGalaxyPortal () {
		galaxyPortalClone = Instantiate (galaxyPortal, galaxyPortal.transform.position, galaxyPortal.transform.rotation) as GameObject;
		galaxyPortalClone.transform.localScale = new Vector3 (0f, galaxyPortalClone.transform.localScale.y, 0f);
		galaxyTwist = true;

		galaxyPortalCloneLight = galaxyPortalClone.GetComponentInChildren<Light> ();
		galaxyPortalCloneLight.intensity = 0f;
	}

	public void startFinalLevel () {
		audioSource [0].enabled = true;
		lockPlayerMovement (true);
		shakeCam ();
		startGalaxyPortal ();
	}

	IEnumerator FadeOutGalaxyRising () {
		for (int i = 0; i < 10; i++) {
			audioSource [0].volume -= 0.1f;
			yield return new WaitForSeconds (0.02f);
		}
		audioSource [1].enabled = false;
		audioSource [0].enabled = false;
		audioSource [2].enabled = true;
		audioSource [3].enabled = true;
	}

	IEnumerator InfinityEnemies () {
		yield return new WaitForSeconds (0.5f);

		enemy1CloneFirst = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (5f);

		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (3f);
		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (3f);
		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (3f);
		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (3f);
		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (0.5f);

		enemy1CloneFirst = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (1f);

		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (1f);
		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (10f);

		enemy2Clone = Instantiate (enemy2, origin, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (3f);

		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (1f);
		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;
		yield return new WaitForSeconds (1f);
		enemy1Clone = Instantiate (enemy1, origin, Quaternion.identity) as GameObject;
		enemy1counter++;

		enemy2Clone = Instantiate (enemy2, origin, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (7f);
		enemy2Clone = Instantiate (enemy2, origin, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (2f);
		enemy2Clone = Instantiate (enemy2, origin, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (30f);

		doneWithEnemies = true;
//		levelBeaten = true;
	}
}
