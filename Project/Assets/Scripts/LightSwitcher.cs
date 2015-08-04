using UnityEngine;
using System.Collections;

public class LightSwitcher : Trigger {
	public GameObject gameManager;
	public GameObject[] lights;

	public override void Activate() {
		activated = true;
		foreach (GameObject light in lights) {
			light.SetActive(false);
		}
		gameManager.SendMessage("StartGame");
	}

	public override void Deactivate() {
		activated = false;
		foreach (GameObject light in lights) {
			light.SetActive(true);
		}
		gameManager.SendMessage("StopGame");
	}
}
