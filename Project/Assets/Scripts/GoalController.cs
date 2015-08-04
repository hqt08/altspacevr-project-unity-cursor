using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {
	private GameManager gameManager;
	public GameObject fireworks;

	void Start() {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" && !gameManager.won) {
			gameManager.won = true;
			StartCoroutine(WinSequence());
		}
	}

	/// <summary>
	/// Win sequence, with fireworks!
	/// </summary>
	IEnumerator WinSequence() {
		fireworks.SetActive(true);
		// to get an aerial view
		Vector3 camPos = Camera.main.gameObject.transform.position;
		camPos = camPos + (transform.position - camPos).normalized * 20f;
		camPos.y += 10f;
		Camera.main.gameObject.transform.position = camPos;
		Camera.main.gameObject.transform.LookAt(transform.position);
		gameManager.audio.SendMessage("PlayWinSound");
		yield return new WaitForSeconds (4f);
		gameManager.Win ();
	}

}
