using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour {
	void Update ()
	{
#if UNITY_5 || UNITY_5_1 || UNITY_5_1_1
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = false;
#else
		Screen.showCursor = true;
#endif

		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
