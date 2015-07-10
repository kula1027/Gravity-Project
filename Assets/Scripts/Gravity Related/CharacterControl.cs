using UnityEngine;
using System.Collections;

public class CharacterControl : GravityEffected {
	private Animator animt;
	private AudioClip aud_jmp;

	float moveSpeed;
	float turnSpeed;
	float jumpPower;
	bool isGrounded;
	float lScale;

	void Start () {
		GE_Start ();

		animt = GetComponent<Animator> ();
		//aud_jmp = GetComponent<AudioSource> ();

		moveSpeed = 8f;
		turnSpeed = 1f;
		jumpPower = 20f;
		isGrounded = true;
		lScale = transform.localScale.x;
	}
	
	void Update () {
		if (!g_control.g_onChange) {
			float input_v = Input.GetAxisRaw ("Vertical");
			float input_h = Input.GetAxisRaw ("Horizontal");

			animt.SetFloat ("Input_h", input_h);
			animt.SetBool ("IsGrounded", isGrounded);
			animt.SetFloat("Speed_v", rb2d.velocity.y);

			Flip (input_h);

			if (isGrounded) {
				if (Input.GetButtonDown ("Jump")) {
					//
					rb2d.velocity -= Physics2D.gravity.normalized * jumpPower;
				}
			} 
			SetVelocity (input_h);
			GravityChange (input_v, input_h);
		}
	}

	void SetVelocity(float input_h){
		if (Physics2D.gravity.x == 0)
			rb2d.velocity = new Vector2(getDir (Physics2D.gravity).x * input_h * moveSpeed, rb2d.velocity.y);
		else
			rb2d.velocity = new Vector2(rb2d.velocity.x, getDir (Physics2D.gravity).y * input_h * moveSpeed);
	}

	private Vector2 getDir(Vector3 gravity){//right direction
		return Vector3.Cross (gravity, new Vector3 (0, 0, -1)).normalized;
	}

	Vector2 g_direction;
	int g_mode = 0;
	void GravityChange(float _input_v, float _input_h){
		if(_input_h == 1 && Input.GetKey (KeyCode.LeftShift)){
			g_direction = getDir(Physics2D.gravity).normalized;

			g_mode = GetGmode (g_direction);
			if(g_mode != g_control.g_currentMode){
				g_control.g_onChange = true;
				animt.SetBool("Gconvert", g_control.g_onChange);
				StartCoroutine("RotateChar", true);
			}
		}
		if(_input_v == 1 && Input.GetKey (KeyCode.LeftShift)){
			g_direction = -Physics2D.gravity.normalized;

			g_mode = GetGmode (g_direction);
			if(g_mode != g_control.g_currentMode){
				g_control.g_onChange = true;
				animt.SetBool("Gconvert", g_control.g_onChange);
				StartCoroutine("RotateChar", false);
			}
		}
		if(_input_h == -1 && Input.GetKey (KeyCode.LeftShift)){
			g_direction = -getDir(Physics2D.gravity).normalized;

			g_mode = GetGmode (g_direction);
			if(g_mode != g_control.g_currentMode){
				g_control.g_onChange = true;
				animt.SetBool("Gconvert", g_control.g_onChange);
				StartCoroutine("RotateChar", false);
			}
		}
	}

	int GetGmode(Vector2 g_direction){
		if(g_direction.Equals(-Vector2.up))
			return 0;
		if(g_direction.Equals(Vector2.right))
			return 1;
		if(g_direction.Equals(Vector2.up))
			return 2;
		if(g_direction.Equals(-Vector2.right))
			return 3;
		
		return -1;
	}
	
	IEnumerator RotateChar(bool rotRight){
		Physics2D.gravity = Vector2.zero;

		Quaternion targetRot = GetTargetRotation (g_mode);

		while(Quaternion.Angle(transform.rotation, targetRot) > 0.2f) {
			//transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
			if(rotRight)
				transform.Rotate(new Vector3(0, 0, turnSpeed));
			else
				transform.Rotate(new Vector3(0, 0, -turnSpeed));
			yield return null;
		}

		g_control.SendMessage("GravityConvert", g_mode);
		animt.SetBool("Gconvert", g_control.g_onChange);
	}

	Quaternion GetTargetRotation(int _g_mode){
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
		}

		return Quaternion.identity;
	}

	void Flip(float _input_h){
		Vector2 localS = transform.localScale;
		if (_input_h == -1) {
			localS.x = -lScale;
		}
		if (_input_h == 1) {
			localS.x = lScale;
		}
		transform.localScale = localS;
	}

	void Die(){
		animt.SetBool ("Die", true);
		Debug.Log ("Game Over, Restart in 5 seconds...");
		Application.LoadLevel (Application.loadedLevel);
	}

	void OnTriggerStay2D(Collider2D coll){
		if(!coll.isTrigger)
			isGrounded = true;
	}

	void OnTriggerExit2D(){
		isGrounded = false;
	}
}


//	void IsGrounded(){
////		Vector3 gNormalized = Physics2D.gravity.normalized;
////		Vector2 rayFrom = transform.position + gNormalized * 0.01f;
////
////		if (Physics2D.Raycast (rayFrom, Physics2D.gravity.normalized, 0.01f) && 
////		    Physics2D.Raycast (rayFrom + getDir(gNormalized) * 0.1f, gNormalized, 0.02f) &&
////		    Physics2D.Raycast (rayFrom - getDir(gNormalized) * 0.1f, gNormalized, 0.02f))
////			isGrounded = true;
////		else
////			isGrounded = false;
//	}
