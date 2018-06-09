using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	protected override void OnRabitHit(HeroRabbit rabbit) {
		// TODO: add actions
		this.CollectedHide ();
	}
}
