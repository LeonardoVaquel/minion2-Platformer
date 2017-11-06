using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int vida;
	public int score;
	private Rigidbody2D rigidbody;
	private BoxCollider2D collider;
	private Animator animator;
	private Vector2 movimiento;
	public bool isJump;
	public float speed;
	public float speedJump;

	// Use this for initialization
	void Start () {
		animator 	= GetComponent<Animator> ();
		rigidbody 	= GetComponent<Rigidbody2D> ();
		collider 	= GetComponent<BoxCollider2D> ();
		movimiento 	= new Vector2 (0, 0);
		isJump 		= false;
		vida 		= 3;
		score 		= 0;
		speed 		= 4f;
		speedJump 	= 4f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		checkMovement ();
		checkAnimation ();
	}

	void checkMovement(){
		movimiento	= new Vector2 (	Input.GetAxisRaw ("Horizontal"), 0);
		rigidbody.MovePosition (rigidbody.position + movimiento * speed * Time.deltaTime);
	}

	void checkAnimation(){
		animator.SetFloat ("movX", rigidbody.velocity.x);
		animator.SetFloat ("movY", rigidbody.velocity.y);

		animator.SetBool ("jumping", isJump);
		
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "suelo") {
			isJump = false;
		}

		if (col.gameObject.tag == "spike" || col.gameObject.tag == "enemigo") {
			recibirDanio();
		}

		if(col.gameObject.tag == "brain"){
			score += 5;
		}
			
	}


	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "suelo") {
			isJump = true;
		}
	}

	void recibirDanio(){
		vida -= 1;
		Debug.Log (vida);
	}
}
