using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioSource audioSource; // for general background music
	public AudioSource playerSource; // for SFX
	public AudioClip audioClipUsual, audioClipGame, audioClipLose, audioClipWin;
	public AudioClip audioClipFirework, audioClipBump;

	/// <summary>
	/// Starts the game music.
	/// </summary>
	void StartGameMusic() {
		RestartSound(audioClipGame, 1f);
	}

	/// <summary>
	/// Starts the usual chill music.
	/// </summary>
	void StartUsualMusic() {
		RestartSound(audioClipUsual, 0.4f);
	}

	/// <summary>
	/// Plays the laser fizz clip.
	/// </summary>
	void PlayLaserFizz() {
		PlaySoundFX(audioClipLose, 1f);
	}

	/// <summary>
	/// Plays the bump clip.
	/// </summary>
	void PlayCartBump() {
		PlaySoundFX(audioClipBump, 1f);
	}

	/// <summary>
	/// Audio for game win
	/// </summary>
	void PlayWinSound() {
		PlaySoundFX(audioClipFirework, 1f);
		RestartSound(audioClipWin, 1f);
	}

	/// <summary>
	/// Function to change sound clips
	/// </summary>
	void RestartSound(AudioClip audioClip, float volume) {
		audioSource.Stop();
		audioSource.clip = audioClip;
		audioSource.loop = false;
		audioSource.volume = volume;
		audioSource.Play();
	}

	/// <summary>
	/// Function to play sound FX at player;
	/// </summary>
	void PlaySoundFX(AudioClip audioClip, float volume) {
		playerSource.Stop();
		playerSource.clip = audioClip;
		playerSource.loop = false;
		playerSource.volume = volume;
		playerSource.Play();
	}
}
