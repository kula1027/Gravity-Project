﻿using UnityEngine;
using System.Collections;

public class CharacterControl : IngameObject {
	private Animator animt;
	private AudioClip aud_jmp;

	float moveSpeed;
	float turnSpeed;
	float jumpPower;
	bool isGrounded;
	float lScale;

	void Start () {
		Obj_Start ();

		animt = GetComponent<Animator> ();
		//aud_jmp = GetComponent<AudioSource> ();

		moveSpeed = 8f;
		turnSpeed = 1f;
		jumpPower = 24f;
		isGrounded = true;
		lScale = transform.localScale.x;
	}
	
	void Update () {
		if (!g_control.G_onChange) {
			float input_h = Input.GetAxisRaw ("Horizontal");

			animt.SetFloat ("Input_h", input_h);
			animt.SetBool ("IsGrounded", isGrounded);
			animt.SetFloat("Speed_v", rb2d.velocity.y);

			Flip (input_h);

			if (isGrounded) {
				if (Input.GetButtonDown ("Jump")) {
					rb2d.velocity -= Physics2D.gravity.normalized * jumpPower;
				}
			} 
			Move (input_h);
		}
	}

	private void Move(float input_h){
		if (Physics2D.gravity.x == 0)
			rb2d.velocity = new Vector2(HELPER.getDir_Right (Physics2D.gravity).x * input_h * moveSpeed, rb2d.velocity.y);
		else
			rb2d.velocity = new Vector2(rb2d.velocity.x, HELPER.getDir_Right (Physics2D.gravity).y * input_h * moveSpeed);
	}

	public void StartRotate(bool rotRight, int _g_mode){
		StartCoroutine (RotateChar (rotRight, _g_mode));
	}

	private IEnumerator RotateChar(bool rotRight, int _g_mode){
		animt.SetBool("isRotating", true);
		Physics2D.gravity = Vector2.zero;

		Quaternion targetRot = GetTargetRotation (_g_mode);

		while(Quaternion.Angle(transform.rotation, targetRot) > 0.2f) {
			if(rotRight)
				transform.Rotate(new Vector3(0, 0, turnSpeed));
			else
				transform.Rotate(new Vector3(0, 0, -turnSpeed));
			yield return null;
		}

		g_control.SendMessage("GravityConvert", _g_mode);
		animt.SetBool("isRotating", false);
	}

	private Quaternion GetTargetRotation(int _g_mode){
		float sqrt05 = Mathf.Sqrt (0.5f);

		switch (_g_mode) {
		case 0:
			return new Quaternion(0, 0, 0, 1);
		case 1:
			return new Quaternion(0, 0, sqrt05, sqrt05);
		case 2:
			return new Quaternion(0, 0, 1, 0);
		case 3:
			return new Quaternion(0, 0, sqrt05, -sqrt05);
		default:
			Debug.Log ("g_mode not valid");
			break;
		}

		return Quaternion.identity;
	}

	private void Flip(float _input_h){
		Vector2 localS = transform.localScale;
		if (_input_h == -1) {
			localS.x = -lScale;
		}
		if (_input_h == 1) {
			localS.x = lScale;
		}
		transform.localScale = localS;
	}

	public void Die(){
		animt.SetBool ("Die", true);
		Debug.Log ("DEAD!");
		StartCoroutine ("RestartInSeconds", 3);
	}

	private IEnumerator RestartInSeconds(float _seconds){
		yield return new WaitForSeconds (_seconds);
		Application.LoadLevel (Application.loadedLevel);
	}

	#region Trigger
	void OnTriggerStay2D(Collider2D coll){
		if(!coll.isTrigger)
			isGrounded = true;
	}

	void OnTriggerExit2D(){
		isGrounded = false;
	}
	#endregion
}

