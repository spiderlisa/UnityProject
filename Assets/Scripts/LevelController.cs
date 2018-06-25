using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	private LevelStat stats;
	
	public static int lives = 3;
	public Sprite EmptyHeart;
	public Sprite EmptyCrystal;
	
	public static LevelController Current;
	private Vector3 startingPosition;
	
	public AudioClip music = null;
	AudioSource musicSource = null;
	
	void Start() {
		musicSource = gameObject.AddComponent<AudioSource>(); 
		musicSource.clip = music;
		musicSource.loop = true;
		musicSource.Play();
		LoadStats();
	}

	void Awake() {
		Current = this;
	}
	
	public void setStartPosition(Vector3 pos) { 
		this.startingPosition = pos;
	}
	
	public void onRabbitDeath(HeroRabbit rabbit) {
		if (lives > 0) {
			rabbit.PlayDeathSound();
			Image crystImg = GameObject.Find("Heart" + lives).GetComponent<Image>();
			crystImg.sprite = EmptyHeart;
			--lives;
			rabbit.transform.position = this.startingPosition;
			rabbit.Live();
		}
		else {
			lives = 3;
			Coins.n = 0;
			Coins.Refresh();
			Fruit.n = 0;
			Fruit.Refresh();
			Crystal.Clean();
			for (int i = 1; i < 4; i++) {
				Image crystImg = GameObject.Find("NonCrystal" + i).GetComponent<Image>();
				crystImg.sprite = EmptyCrystal;
			}
			LoseLevel.Instance.Show();
		}
	}

	public void SetLevelPassed() {
		stats.hasCrystals = Crystal.AllCollected();
		stats.hasAllFruits = (Fruit.All == Fruit.n);
		stats.levelPassed = true;
		PlayerPrefs.SetString(SceneManager.GetActiveScene().name, JsonUtility.ToJson(this.stats));
		PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + Coins.n);
		PlayerPrefs.Save();
	}

	public bool LevelIsPassed() {
		return stats.levelPassed;
	}
	
	public void LoadStats() {
		this.stats = LevelStat.ReadInfo(SceneManager.GetActiveScene().name);
		if (this.stats == null) this.stats = new LevelStat();
	}

}
