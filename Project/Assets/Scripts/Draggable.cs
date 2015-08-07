using UnityEngine;
using System.Collections;

public class Draggable : Trigger {
	private float zdepth;

	public override void Update() {
		base.Update();
		if (activated) {
			// object will be moved to the indicated mouse position on the same z plane
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, zdepth));
		}
	}

	public override void Activate() {
		zdepth = Mathf.Abs(transform.position.z - Camera.main.transform.position.z); // keep track of the z-distance offset
		activated = true;
	}

	public override void Deactivate() {
		activated = false;
	}
}
