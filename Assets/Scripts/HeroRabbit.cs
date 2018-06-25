using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroRabbit : MonoBehaviour {
	
	public static HeroRabbit LastRabbit = null;

	public bool Blocked = false;
	public float Speed = 2f;
	public float MaxJumpTime = 2f; 
	public float JumpSpeed = 3f;

	private Rigidbody2D myBody = null;
	private Animator animator = null;
	private Transform heroParent = null;

	public AudioClip dieSound = null;
	public AudioClip runSound = null;
	public AudioClip landSound = null;
	private AudioSource dieSource = null;
	private AudioSource runSource = null;
	private AudioSource landSource = null;
	
	bool isGrounded = false;
	bool jumpActive = false;
	float jumpTime = 0f;
	bool isBig = false;
	float deathTimeOut = 0f;

	void Start() {
		if (!Blocked) {
			myBody = this.GetComponent<Rigidbody2D>();
			this.heroParent = this.transform.parent;
			LastRabbit = this;
			LevelController.Current.setStartPosition(transform.position);
			
			dieSource = gameObject.AddComponent<AudioSource>(); 
			dieSource.clip = dieSound;
			runSource = gameObject.AddComponent<AudioSource>(); 
			runSource.clip = runSound;
			landSource = gameObject.AddComponent<AudioSource>(); 
			landSource.clip = landSound;
		}
	}

	void FixedUpdate() {
		if (!Blocked) {
			float value = Input.GetAxis("Horizontal");
			animator = GetComponent<Animator>();

			deathTimeOut -= Time.deltaTime;
			if (animator.GetBool("death") && deathTimeOut <= 0) {
				LevelController.Current.onRabbitDeath(this);
			}

			// Running
			if (Mathf.Abs(value) > 0) {
				Vector2 vel = myBody.velocity;
				vel.x = value * Speed;
				myBody.velocity = vel;
				animator.SetBool("run", true);
				if (!runSource.isPlaying && SoundManager.Instance.IsSoundOn()) 
					runSource.Play();
			}
			else {
				runSource.Stop();
				animator.SetBool("run", false);
			}
			if (!isGrounded) runSource.Stop();


			// Checking if on the ground (& moving platform)
			Vector3 from = transform.position + Vector3.up * 0.3f;
			Vector3 to = transform.position + Vector3.down * 0.1f;
			int layer_id = 1 << LayerMask.NameToLayer("Ground");

			RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
			if (hit) {
				if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null) {
					SetNewParent(this.transform, hit.transform);
				}
				this.isGrounded = true;
			}
			else {
				SetNewParent(this.transform, this.heroParent);
				this.isGrounded = false;
				
				if (SoundManager.Instance.IsSoundOn())
					landSource.Play();
			}


			// Jumping
			if (Input.GetButtonDown("Jump") && this.isGrounded) {
				this.jumpActive = true;
			}

			if (this.jumpActive) {
				if (Input.GetButton("Jump")) {
					this.jumpTime += Time.deltaTime;
					if (this.jumpTime < this.MaxJumpTime) {
						Vector2 vel = myBody.velocity;
						vel.y = JumpSpeed * (1.0f - jumpTime / MaxJumpTime);
						myBody.velocity = vel;
					}
				}
				else {
					this.jumpActive = false;
					this.jumpTime = 0f;
				}
			}

			if (this.isGrounded) {
				animator.SetBool("jump", false);
			}
			else {
				animator.SetBool("jump", true);
			}


			// Flips
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			if (value < 0) {
				sr.flipX = true;
			}
			else if (value > 0) {
				sr.flipX = false;
			}
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
		deathTimeOut = 2f;
	}

	public void PlayDeathSound() {
		if (SoundManager.Instance.IsSoundOn())
			dieSource.Play();
	}
	
	static void SetNewParent(Transform obj, Transform new_parent) { 
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos; 
		}
	}

	public bool GetIsBig() {
		return isBig;
	}
}
