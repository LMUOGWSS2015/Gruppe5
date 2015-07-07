using UnityEngine;
using System.Collections;

public class PauseCanvas : CaPCanvas {
	ScreenShake ss;
	float shake;
	bool pause = false;

	void Awake(){
		ss = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ScreenShake> ();
	}

	public override void Show (bool show) {
		base.Show (show);
		pause = show;

		if (show) {
			Time.timeScale = 0;
			shake = ss.GetShake();
			ss.SetShake(0.0f);
		} else {
			Time.timeScale = 1;
			ss.SetShake(shake);
		}
	}

	public bool GetPause(){
		return pause;
	}
}
