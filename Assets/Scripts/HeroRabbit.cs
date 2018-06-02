using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	public float speed = 2f;
	public float MaxJumpTime = 2f; 
	public float JumpSpeed = 3f;

	private Rigidbody2D myBody = null;
	private Animator animator = null;
	
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;

	void Start() {
		myBody = this.GetComponent<Rigidbody2D>();
		LevelController.current.setStartPosition (transform.position);
	}

	void Update() { }

	void FixedUpdate() {
		float value = Input.GetAxis("Horizontal");
		animator = GetComponent<Animator> ();
		
		// Running
		if (Mathf.Abs(value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
			animator.SetBool("run", true);
		} else {
			animator.SetBool("run", false); 
		}
		
		
		// Checking if on the ground
		Vector3 from = transform.position + Vector3.up * 0.3f; 
		Vector3 to = transform.position + Vector3.down * 0.1f; 
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");
		
		RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
		if (hit) {
			this.isGrounded = true;
		} else {
			this.isGrounded = false;
		}
		Debug.DrawLine (from, to, Color.red);
		
		
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
}
