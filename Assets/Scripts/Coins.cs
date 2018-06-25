using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : Collectable {

	public static int n = 0;

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		++n;
		Refresh();
		this.CollectedHide ();
	}

	public static void Refresh() {
		Text count = GameObject.Find("CoinCount").GetComponent<Text>();
		if (n.ToString().Length==1)
			count.text = "000" + n;
		else if (n.ToString().Length==2)
			count.text = "00" + n;
		else if (n.ToString().Length==3)
			count.text = "0" + n;
		else
			count.text = n.ToString();
	}
}
