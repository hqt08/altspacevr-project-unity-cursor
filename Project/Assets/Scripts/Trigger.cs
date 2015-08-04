using UnityEngine;
using System.Collections;

// The GameObject requires a Selectable component
[RequireComponent (typeof (Selectable))]
public class Trigger : MonoBehaviour {
	// has this object been activated	
	protected bool activated = false;

	void Update() {
		if (Input.GetMouseButtonDown(0) && gameObject == Selectable.CurrentSelection) {
			if (activated) {
				Deactivate();
				activated = false;
			} 
			else  {
				Activate();
				activated = true;
			}
		}
	}

	/// <summary>
	/// Override these activation functions in trigger subclass
	/// </summary>
	public virtual void Activate() {}
	public virtual void Deactivate() {}
}
