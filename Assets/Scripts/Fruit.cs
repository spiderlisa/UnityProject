using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : Collectable {

	public static int All = 11;
	public static int n = 0;

	public int Image = 1; // 1 - Cherry, 2 - Grapes, 3 - Apple
	
	protected override void OnRabbitHit(HeroRabbit rabbit) {
		++n;
		this.CollectedHide();
		Refresh();
	}

	public static void Refresh() {
		Text count = GameObject.Find("FruitCount").GetComponent<Text>();
		count.text = n + "/" + All;
	}

}
