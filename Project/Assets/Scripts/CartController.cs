using UnityEngine;
using System.Collections;

public class CartController : MonoBehaviour {
	public float speed = 1;
	public Transform startPos;
	public Transform endPos;
	private GameManager gameManager;
	private bool bumped;
	
	void Start() {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void Update() {
		transform.position += transform.forward.normalized * speed;
		if ((transform.position - endPos.position).magnitude < 1) Restart();
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player" && !bumped && !gameManager.won) {
			bumped = true;
			StartCoroutine(BumpSequence(other.gameObject));
		}
	}
	
	void Restart() {
		gameObject.transform.position = startPos.position;
	}

	/// <summary>
	/// Bump effect sequence.
	/// </summary>
	IEnumerator BumpSequence(GameObject player) {
		gameManager.GetComponent<GameManager>().audio.SendMessage("PlayCartBump");
		GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
		yield return new WaitForSeconds (2f);
		GameObject.Find("Main Camera").GetComponent<Animator>().enabled = false;
		gameManager.GameOver();
	}
	
}