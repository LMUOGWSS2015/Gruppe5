using UnityEngine;
using System.Collections;

public class PauseCanvas : CaPCanvas {

	public override void Show (bool show) {
		base.Show (show);

		if (show)
			Time.timeScale = 0;
		else 
			Time.timeScale = 1;
	}
}
