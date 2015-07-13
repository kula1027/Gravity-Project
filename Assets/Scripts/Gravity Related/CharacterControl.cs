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
		jumpPower = 24f;
		isGrounded = true;
		lScale = transform.localScale.x;
	}
	
	void Update () {
		if (!g_control.G_onChange) {
			float input_v = Input.GetAxisRaw ("Vertical");
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

	void Move(float input_h){
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
		default:
			Debug.Log ("g_mode not valid");
			break;
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
		Debug.Log ("DEAD!");
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
