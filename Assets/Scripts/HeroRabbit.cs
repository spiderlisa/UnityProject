using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	public float speed = 2f;
	public float MaxJumpTime = 2f; 
	public float JumpSpeed = 3f;

	private Rigidbody2D myBody = null;
	private Animator animator = null;
	private Transform heroParent = null;
	
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public bool isBig = false;

	private float death_time_out = 0f;

	void Start() {
		myBody = this.GetComponent<Rigidbody2D>();
		LevelController.current.setStartPosition (transform.position);
		this.heroParent = this.transform.parent;
	}

	void FixedUpdate() {
		float value = Input.GetAxis("Horizontal");
		animator = GetComponent<Animator>();

		death_time_out -= Time.deltaTime;
		if (animator.GetBool("death") && death_time_out <= 0)
		{
			LevelController.current.onRabbitDeath(this);
		}
		
		// Running
		if (Mathf.Abs(value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
			animator.SetBool("run", true);
		} else {
			animator.SetBool("run", false); 
		}
		
		
		// Checking if on the ground (& moving platform)
		Vector3 from = transform.position + Vector3.up * 0.3f; 
		Vector3 to = transform.position + Vector3.down * 0.1f; 
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");
		
		RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
		if (hit) {
			if(hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null) { 
				SetNewParent(this.transform, hit.transform);
			}
			this.isGrounded = true;
		} else {
			SetNewParent(this.transform, this.heroParent);
			this.isGrounded = false;
		}
		//Debug.DrawLine (from, to, Color.red);
		
		
		// Jumping
		if(Input.GetButtonDown("Jump") && this.isGrounded) {
			this.JumpActive = true; 
		}
		
		if(this.JumpActive) {
			if(Input.GetButton("Jump")) {
				this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime); 
					myBody.velocity = vel;
				}
			} else {
			this.JumpActive = false;
			this.JumpTime = 0f; 
			}
		}
		
		if(this.isGrounded) {
			animator.SetBool("jump", false);
		} else {
			animator.SetBool("jump", true);
		}
		
		
		// Flips
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (value < 0) {
			sr.flipX = true;
		} else if (value > 0) {
			sr.flipX = false;
		}
	
	}

	public void ChangeSize() {
		Vector3 scale = this.transform.localScale;
		if (isBig) {
			scale.x = 1f;
			scale.y = 1f;
			isBig = false;
		} else {
			scale.x = 1.5f;
			scale.y = 1.5f;
			isBig = true;
			
			// TODO: Потрібно зробити, щоб наступні 4 секунди на нього не діяли інші бомби (він просто міг проходити скрізь них).
			// TODO: Поки тривають 4 секунди кролик має підсвічуватись червоним.
		}
		this.transform.localScale = scale;
	}

	public void Live() {
		animator = GetComponent<Animator>();
		animator.SetBool("death", false);
		animator.SetTrigger("reset");
		if (isBig) 
			ChangeSize();
	}

	public void Die() {
		animator = GetComponent<Animator>();
		animator.SetBool("death", true);
		death_time_out = 2f;
	}
	
	static void SetNewParent(Transform obj, Transform new_parent) { 
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos; 
		}
	}
}
