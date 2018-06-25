using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {
	
	public float WalkSpeed = 1f;
	public float Delta = 5f;
	
	protected SpriteRenderer sr = null;
	protected Animator animator = null;
	
	protected Mode mode;
	protected Vector3 pointA, pointB;

	public static float rabbitDeathTimeOut = 0f;
	public static bool rabbitDead = false;
	
	public AudioClip dieSound = null;
	private AudioSource dieSource = null;
	
	protected enum Mode {
		GoToA,
		GoToB,
		Attack,
		Dead
	}
	
	void Start () {
		pointB = this.transform.position;
		pointA = pointB;
		pointA.x += Delta;
		
		//Debug.Log("A: " + pointA.x + " " + pointA.y);
		//Debug.Log("B: " + pointB.x + " " + pointB.y);

		sr = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		
		dieSource = gameObject.AddComponent<AudioSource>(); 
		dieSource.clip = dieSound;
	}

	void FixedUpdate() {
		if (mode != Mode.Dead) {
			if (!rabbitDead) {
				if (RabbitIsInZone()) {
					mode = Mode.Attack;
					AttackRabbit();
				} else {
					if (mode == Mode.GoToA) {
						if (IsArrived(pointA)) {
							mode = Mode.GoToB;
						}
					} else if (mode == Mode.GoToB) {
						if (IsArrived(pointB)) {
							mode = Mode.GoToA;
						}
					} else {
						mode = Mode.GoToA;
					}
					Patrol();
				}
			} else if (rabbitDeathTimeOut > 0) {
				rabbitDeathTimeOut -= Time.deltaTime;
			} else {
				rabbitDead = false;
				this.transform.position = pointB;
			}
		}
	}
	
	protected virtual bool RabbitIsInZone() {
		Vector3 my_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabbit.LastRabbit.transform.position; 
		return rabbit_pos.x > Mathf.Min(pointA.x, pointB.x) && rabbit_pos.x < Mathf.Max(pointA.x, pointB.x) && Math.Abs(rabbit_pos.y-my_pos.y) < 0.5f;;
	}
	
	protected virtual void AttackRabbit() {
		
	}

	private void Patrol() {
		animator.SetBool("walk", true);
		float value = this.GetDirection();
		transform.position += new Vector3(value, .0f, .0f) * WalkSpeed * Time.deltaTime;
	}
	
	private bool IsArrived(Vector3 point) {	
		Vector3 my_pos = this.transform.position;
		return Math.Abs(my_pos.x-point.x)<0.05f && Math.Abs(my_pos.y-point.y)<0.5f;
	}

	private float GetDirection() {
		Vector3 my_pos = this.transform.position;
		if (mode == Mode.GoToA) {
			if (my_pos.x > pointA.x) {
				sr.flipX = false;
				return -1;
			}
			sr.flipX = true;
			return 1;
		}
		if (mode == Mode.GoToB) {
			if (my_pos.x > pointB.x) {
				sr.flipX = false;
				return -1;
			}
			sr.flipX = true;
			return 1;
		}
		return 0;
	}

	protected float GetDirectionToRabbit() {
		Vector3 rabbit_pos = HeroRabbit.LastRabbit.transform.position;
		Vector3 my_pos = this.transform.position;
		if (mode == Mode.Attack) {
			if (my_pos.x < rabbit_pos.x) {
				sr.flipX = true;
				return -1;
			}

			sr.flipX = false;
			return 1;
		}

		return 0;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (this.isActiveAndEnabled) {
			HeroRabbit rabbit = collision.gameObject.GetComponent<HeroRabbit>();
			if (rabbit != null) {
				this.OnRabbitHit(rabbit);
			}
		}
	}
	
	void OnRabbitHit(HeroRabbit rabbit) {
		Vector3 v = rabbit.transform.position - transform.position;
		float angle = Mathf.Atan2(v.y, v.x) / Mathf.PI * 180;
		if (angle > 45f && angle < 135f) {
			Die();
		} else {
			animator.SetTrigger("attack");
			rabbit.Die();
			
			rabbitDeathTimeOut = 2.1f;
			rabbitDead = true;
		}
	}

	private void Die() {
		mode = Mode.Dead;
		animator.SetTrigger("death");
		dieSource.Play();
		StartCoroutine(Death());
	}
	
	private IEnumerator Death() {
		yield return new WaitForSeconds(2f);
		Destroy(this.gameObject);
	}
}
