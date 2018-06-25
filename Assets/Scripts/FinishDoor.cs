using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoor : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if (this.isActiveAndEnabled) {
			HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
			if (rabbit != null) {
				LevelController.Current.SetLevelPassed();
				WinLevel.Instance.Show();
			}
		}
	}
}
