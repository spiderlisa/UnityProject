using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : Collectable {

	public static bool[] collected;

	public int mode = 1; // 1 - blue, 2 - green, 3 - red
	public Sprite CrystalSprite;
	
	protected override void OnRabbitHit(HeroRabbit rabbit) {
		this.CollectedHide();
		
		if (collected==null) 
			collected = new bool[3];

		collected[mode - 1] = true;
		Image crystImg = GameObject.Find("NonCrystal" + mode).GetComponent<Image>();
		crystImg.sprite = CrystalSprite;
	}

	public static void Clean() {
		collected = new bool[3];
	}

	public static bool AllCollected() {
		if (collected == null) 
			return false;
		
		for (int i = 0; i < 3; i++) {
			if (!collected[i])
				return false;
		}
		
		return true;
	}
	 
}
