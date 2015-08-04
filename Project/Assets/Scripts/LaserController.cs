using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {
	
	public float yOffset = 2f; // y offset for laser, if any
 	public float range = 5f; // range of laser
	public float appearSpeed = 3f; // speed at which the laser appears
	public float speed = 5f; // speed at which the laser rotates
	public Vector3 newRot; // alternate between this rotation 
	private Vector3 originalRot; // and the original rotation
	private Vector3 originalPos;

	private GameManager gameManager; // game manager
	private RaycastHit shootHit; // Raycast Hit
	private const int shootableLayerMask = (1 << 10); // This is the layer mask to use for only shootable objects
	private const float knockbackStrength = 10f; // Amount of laser knockback
	
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		originalPos = transform.position;
		originalRot = transform.rotation.eulerAngles;
		transform.position = new Vector3(originalPos.x, originalPos.y - yOffset, originalPos.z); // start off with some y offset
		Appear();
	}

	void Update() {
		if (!gameManager.won) {
			if (Physics.Raycast(transform.position, transform.forward, out shootHit, range, shootableLayerMask))
			{
				Debug.Log("Hit " + shootHit.collider.gameObject.name);
				// laser tracking, check if player was hit
				if (shootHit.collider.gameObject.tag == "Player") {
					StartCoroutine(GameOverSequence(shootHit.collider.gameObject));
				}
			}
		}
	}
		
	/// <summary>
	/// Function for when the laser first appears
	/// </summary>
	void Appear() {
		iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", appearSpeed, "easetype", iTween.EaseType.easeInOutBounce, "oncomplete", "AltRotate"));
	}

	/// <summary>
	/// Rotation function
	/// </summary>
	void AltRotate () {
		iTween.RotateTo(gameObject, iTween.Hash("rotation", newRot, "time", speed, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
	}

	/// <summary>
	/// Game over sequence.
	/// </summary>
	IEnumerator GameOverSequence(GameObject player) {
		Vector3 direction = player.GetComponent<CharacterController>().velocity.normalized;
		gameManager.GetComponent<GameManager>().audio.SendMessage("PlayLaserFizz");
		iTween.MoveBy (player, iTween.Hash("amount", -direction * knockbackStrength, "time", 1f, "space", "world"));
		yield return new WaitForSeconds (1f);
		gameManager.GameOver();
	}

}
