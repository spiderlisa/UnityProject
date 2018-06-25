using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

	public static Settings Instance;

	public Sprite MusicOn;
	public Sprite MusicOff;
	public Sprite SoundOn;
	public Sprite SoundOff;

	public Button MusicBtn;
	public Button SoundBtn;

	private bool musicIsOn = true;
	private bool soundIsOn = true;
    
	void Awake() {
		gameObject.SetActive(false);
	}
    
	void Start () {
		Instance = this;
		soundIsOn = SoundManager.Instance.IsSoundOn();
		musicIsOn = SoundManager.Instance.IsMusicOn();
		SoundBtn.image.sprite = soundIsOn ? SoundOn : SoundOff;
		MusicBtn.image.sprite = musicIsOn ? MusicOn : MusicOff;
	}
	
	public void MusicClicked() {
		musicIsOn = !musicIsOn;
		SoundManager.Instance.SetMusicOn(musicIsOn);
		MusicBtn.image.sprite = musicIsOn ? MusicOn : MusicOff;
	}

	public void SoundClicked() {
		soundIsOn = !soundIsOn;
		SoundManager.Instance.SetSoundOn(soundIsOn);
		SoundBtn.image.sprite = soundIsOn ? SoundOn : SoundOff;
	}
    
	public void Hide() {
		HeroRabbit rabbit = HeroRabbit.LastRabbit;
		if(rabbit != null)
			rabbit.Blocked = false;
		
		gameObject.SetActive(false);
	}

	public void Show() {
		HeroRabbit rabbit = HeroRabbit.LastRabbit;
		if (rabbit != null)
			rabbit.Blocked = true;
		
		gameObject.SetActive(true);
	}
}
