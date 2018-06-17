using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : Orc {

	public float runSpeed = 3f;

	private float attackTimeOut;

	protected override void AttackRabbit() {
		if (!rabbitDead) {
			animator.SetBool("walk", false); // triggers running
			transform.position -= new Vector3(GetDirectionToRabbit(), .0f, .0f) * runSpeed * Time.deltaTime;
		}
	}

}
