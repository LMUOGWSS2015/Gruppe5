using UnityEngine;
using System.Collections;

public class ControlCanvasScript : CaPCanvas {
	public Canvas mainMenu;

	public override void Show (bool show) {
		base.Show (show);
		mainMenu.gameObject.SetActive (!show);
	}
}
