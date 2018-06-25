using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour {

	public GameObject settings;

	public void StartPlay() {
		SceneManager.LoadScene("LevelGuide");
	}

	public void ShowSettings() {
		GameObject obj = GameObject.Instantiate(this.settings);
		Settings sett = obj.GetComponent<Settings>();
		sett.Show();
	}
	
}
