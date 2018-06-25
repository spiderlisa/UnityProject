using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {
	
	public float Speed = 3f;
	
	private Vector3 direction;

	void Start () {
		StartCoroutine(DestroyLater());
	}
	
	void Update () {
		transform.position -= direction*Speed*Time.deltaTime;
	}
	
	IEnumerator DestroyLater() {
		yield return new WaitForSeconds(3.0f); 
		Destroy(this.gameObject);
	}
	
	public void Launch(float dir) {
		direction = new Vector3(dir, 0, 0);
		transform.localScale = new Vector3((dir > 0) ? -1 : 1, transform.localScale.y, 1);
	}
	
	protected override void OnRabbitHit(HeroRabbit rabbit) {
		Destroy(this.gameObject);
		rabbit.Die();
		Orc.rabbitDeathTimeOut = 2f;
		Orc.rabbitDead = true;
	}
	
}
