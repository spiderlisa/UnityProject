﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		if (rabbit.GetIsBig()) {
			rabbit.ChangeSize();
		} else {
			rabbit.Die();
		}
		this.CollectedHide ();
	}
}
