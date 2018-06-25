using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLevel : MonoBehaviour {

	public AudioClip winSound;
	private AudioSource winSource;
    
	public static WinLevel Instance = null;

	private void Awake() {
		Instance = this;
		winSource = gameObject.AddComponent<AudioSource>();
		winSource.clip = winSound;
		winSource.playOnAwake = false;
		gameObject.SetActive(false);
	}
	
	public void Show() {
		gameObject.SetActive(true);
		if (SoundManager.Instance.IsSoundOn()) 
			winSource.Play();
		
		Text countFruit = GameObject.Find("Text Fruits").GetComponent<Text>();
		countFruit.text = Fruit.n + "/" + Fruit.All;
		
		Text countCoin = GameObject.Find("Text Coins").GetComponent<Text>();
		countCoin.text = "+" + Coins.n;
	}
    
	public void Repeat() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void Menu() {
		SceneManager.LoadScene("MainMenu");
	}

}
