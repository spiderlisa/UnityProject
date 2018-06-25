﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	
	public AudioClip collectSound = null;
	protected AudioSource collectSource = null;
	
	void Start() {
		collectSource = gameObject.AddComponent<AudioSource>(); 
		collectSource.clip = collectSound;
	}

	protected virtual void OnRabbitHit(HeroRabbit rabbit) {
		
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if(this.isActiveAndEnabled) {
			HeroRabbit rabbit = collider.GetComponent<HeroRabbit>(); 
			if(rabbit != null) {
				this.OnRabbitHit(rabbit); 
			}
		} 
	}
	
	public void CollectedHide() { 
		if (SoundManager.Instance.IsSoundOn())
			collectSource.Play();
		Destroy(this.gameObject);
	}
}
