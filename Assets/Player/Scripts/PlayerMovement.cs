﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 5000.0f;
	public float jumpSpeed = 50000.0f;
	
	public Vector2 direction;

	private Rigidbody2D rigidbody2D;
	private PlayerData playerData;
	private Animator animator;
	
	private int ID;
	
	private bool isGrounded = false;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		playerData = GetComponent<PlayerData>();
		animator = GetComponent<Animator>();
		
		ID = playerData.ID;
		
		direction = transform.right;
	}
	
	// Update is called once per frame
	void Update () {
		HandleGamepadInput();
		HandleKeyboardInput();
		
		animator.SetFloat("Speed", rigidbody2D.velocity.magnitude);
	}
	
	void HandleGamepadInput() {
		// Movment
		float horz = Input.GetAxis ("P" + ID.ToString() + " Left Stick Horizontal");
		//float vert = Input.GetAxis ("P" + ID.ToString() + " Left Stick Vertical");		// dont' really care about this... FOR NOW!
		
		rigidbody2D.AddForce(transform.right * moveSpeed * horz); // Move with force to left
		direction = (transform.right * horz).normalized; // Set facing direction to left
		
		// set animation direction
		if((horz < 0 && transform.localScale.x < 0) || (horz > 0 && transform.localScale.x > 0)) {
			transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
		}
		
		// Jump
		if(isGrounded && Input.GetButtonDown("P" + ID.ToString() + " A")) {
			Debug.Log ("Jumping");
			rigidbody2D.AddForce(transform.up * jumpSpeed);
			isGrounded = false;
		}
	}
	
	void HandleKeyboardInput() {
		// Left Movement
		if(Input.GetKey(KeyCode.A)) {
			rigidbody2D.AddForce(-transform.right * moveSpeed * Time.deltaTime); // Move with force to left
			direction = -transform.right; // Set facing direction to left
		}
		// Right Movement
		if(Input.GetKey(KeyCode.D)) {
			rigidbody2D.AddForce(transform.right * moveSpeed * Time.deltaTime); // Move with force to right
			direction = transform.right; // Set facing direction to right
		}
		// Jump
		if(isGrounded && Input.GetKeyDown(KeyCode.W)) {
			rigidbody2D.AddForce(transform.up * jumpSpeed * Time.deltaTime);
			isGrounded = false;
		}
	}


			
	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Floor") {
			isGrounded = true;
		}
	}
}
