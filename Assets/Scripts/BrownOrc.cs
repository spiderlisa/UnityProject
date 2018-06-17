using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Orc {

	public float distance = 3f;
	public GameObject prefabCarrot;

	private float lastCarrot;
	
	protected override void AttackRabbit() {
		if (!rabbitDead) {
			GetDirectionToRabbit();
			animator.SetBool("walk", false);

			if (Time.time - lastCarrot > 2.0f) {
				lastCarrot = Time.time;
				this.LaunchCarrot(GetDirectionToRabbit());					
				animator.SetTrigger("attack");
			}
		}
	}
	
	protected override bool RabbitIsInZone() {
		Vector3 my_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
		return Math.Abs(rabbit_pos.x-my_pos.x) < distance && Math.Abs(rabbit_pos.y-my_pos.y) < 0.5f;
	}
	
	void LaunchCarrot(float direction) {
		GameObject obj = GameObject.Instantiate(this.prefabCarrot);
		obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+0.7f, .0f);
		Carrot carrot = obj.GetComponent<Carrot>();
		carrot.Launch(direction); 
	}
	
}
