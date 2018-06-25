using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseLevel : MonoBehaviour {

	public AudioClip loseClip;
	AudioSource loseSource;
	
	static public LoseLevel Instance = null;

	private void Awake() {
		Instance = this;
		loseSource = gameObject.AddComponent<AudioSource>();
		loseSource.clip = loseClip;
		loseSource.playOnAwake = false;
		gameObject.SetActive(false);
	}

	public void Show() {
		gameObject.SetActive(true);
		if (SoundManager.Instance.IsSoundOn()) 
			loseSource.Play();
	}

	public void Repeat() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Menu() {
		SceneManager.LoadScene("MainMenu");
	}

}
