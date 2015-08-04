using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject audio; // audio manager
	public GameObject LightController; // Light commands
	public GameObject ObstacleCoursePrefab; // Game course prefab
	private GameObject ObstacleCourse; // Game
	public float ObstacleCourseOffset; // Game Offset
	public GameObject player; // Player
	public GameObject camera; // Main Camera
	public Transform CouchPositionPlayer; // End player position after game
	public Transform CouchPositionCamera; // End camera position after game
	public float originalAmbience = 0.25f; // usual ambient lighting
	public float gameAmbience = 0f; // game ambient lighting
	public bool won; // has game been won

	/// <summary>
	/// Starts the game.
	/// </summary>
	public void StartGame() {
		won = false;
		ChangeAmbientLighting(gameAmbience);
		audio.SendMessage("StartGameMusic");
		SetUpObstacleCourse();
		ObstacleCourse.SetActive(true);
	}

	/// <summary>
	/// Stops the game.
	/// </summary>
	public void StopGame() {
		ChangeAmbientLighting(originalAmbience);
		audio.SendMessage("StartUsualMusic");
		Destroy(ObstacleCourse);
	}

	/// <summary>
	/// Game over sequence.
	/// </summary>
	public void GameOver() {
		LightController.SendMessage("Deactivate");
		ObstacleCourse.SetActive(false);
		CouchPosition();
	}

	/// <summary>
	/// Win sequence.
	/// </summary>
	public void Win() {
		LightController.SendMessage("Deactivate");
		ObstacleCourse.SetActive(false);
		CouchPosition();
	}

	/// <summary>
	/// Instantiates the game course.
	/// </summary>
	public void SetUpObstacleCourse() {
		ObstacleCourse = (GameObject) Instantiate(ObstacleCoursePrefab);
		ObstacleCourse.transform.position = player.transform.position + player.transform.forward.normalized * ObstacleCourseOffset;
		Vector3 relativePos = ObstacleCourse.transform.position - player.transform.position;
		ObstacleCourse.transform.rotation = Quaternion.LookRotation(relativePos);
	}

	/// <summary>
	/// Moves the player back to the couch, as if getting up from a dream
	/// </summary>
	private void CouchPosition() {
		player.transform.position = CouchPositionPlayer.position;
		player.transform.rotation = CouchPositionPlayer.rotation;
		camera.transform.position = CouchPositionCamera.position;
		camera.transform.rotation = CouchPositionCamera.rotation;
	}

	/// <summary>
	/// Changes the ambient lighting intensity.
	/// </summary>
	private void ChangeAmbientLighting(float val) {
		RenderSettings.ambientLight = new Color(val,val,val);
	}
	
}
