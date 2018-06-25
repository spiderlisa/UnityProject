using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHere : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		HeroRabbit rabit = collider.GetComponent<HeroRabbit> ();
		if(rabit != null) {
			LevelController.Current.onRabbitDeath(rabit);
		}
	}
}
