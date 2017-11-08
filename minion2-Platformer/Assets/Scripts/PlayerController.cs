using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public int vida;
	public int score;
	public Rigidbody2D rigidbody;
	public BoxCollider2D collider;
	public Animator animator;
	private Vector2 movimiento;
	public bool isJump;
	public bool doubleJump;
	public bool grounded;
	public float speed;
	public float maxSpeed;
	public float jumpPower;
	public bool invulnerability;
	public int timeOfInvulnerability;
	public Text UIscore;
	public Image UIvida;

	// Use this for initialization
	void Start () {
		animator 				= GetComponent<Animator> ();
		rigidbody 				= GetComponent<Rigidbody2D> ();
		collider 				= GetComponent<BoxCollider2D> ();
		UIvida 					= GameObject.Find ("VidaPanel").GetComponent<Image> ();
		movimiento 				= new Vector2 (0, 0);
		isJump 					= false;
		grounded 				= true;
		vida 					= 3;
		score 					= 0;
		speed 					= 40.7f;
		maxSpeed 				= 5.6f;
		jumpPower 				= 11.5f;
		invulnerability			= false;
		timeOfInvulnerability 	= 20000;
		animator.SetInteger ("vida", vida);
	}
	
	// Update is called once per frame
	void Update(){
		if (grounded) {
			doubleJump = true;
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			if (grounded) {
				isJump = true;
				doubleJump = true;
			}else if(doubleJump){
				isJump = true;
				doubleJump = false;
			}
		}
	}

	void FixedUpdate () {
		checkMovement ();
		checkAnimation ();
	}

	void checkMovement(){
		float movHorizontal = Input.GetAxis ("Horizontal");
		this.rigidbody.AddForce (Vector2.right * speed * movHorizontal);

		float limitedSpeed = Mathf.Clamp (this.rigidbody.velocity.x, -maxSpeed, maxSpeed);
		this.rigidbody.velocity = new Vector2 (limitedSpeed, this.rigidbody.velocity.y);

		//movimiento	= new Vector2 (Input.GetAxisRaw ("Horizontal"), 0);
		//this.rigidbody.MovePosition (this.rigidbody.position + movimiento * speed * Time.deltaTime);

		if (isJump) {
			this.rigidbody.AddForce (Vector2.up * jumpPower, ForceMode2D.Impulse);
			isJump = false;
		}
	}

	void checkAnimation(){
		animator.SetFloat ("posX", Input.GetAxisRaw ("Horizontal"));
		animator.SetFloat ("posY", Input.GetAxisRaw ("Vertical"));
		animator.SetBool ("jumping", isJump);
	}
		

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "suelo") {grounded = true;}			
	}

	void OnTriggerStay2D(Collider2D col){
		
		if(invulnerability){
			//while (timeOfInvulnerability > 0){ Debug.Log ("TIEMPO INVULNERABLE "+timeOfInvulnerability);timeOfInvulnerability -= 1;}
			invulnerability = false;
			animator.SetBool ("invulnerability", false);
			timeOfInvulnerability = 200;
		}else {
			if (col.gameObject.tag == "spike" || col.gameObject.tag == "enemigo") {
				recibirDanio ();
			} 			
		}

		if(col.gameObject.tag == "brain"){
			sumarPunto ();
			Destroy (col.gameObject);
		}
	}

	public void sumarPunto(){
		score += 5;
		UIscore.text = score.ToString();
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "suelo") {
			grounded = false;
		}
	}

	public void recibirDanio(){
		if (vida > 0) {
			vida -= 1;
			cambiarVidaUI ();
			invulnerability = true;
			animator.SetInteger ("vida", vida);
			animator.SetBool ("invulnerability", true);
		}
	}

	void cambiarVidaUI(){
		switch (vida){
			case 0:
				UIvida.sprite = Resources.Load<Sprite> ("Sprites/UI/HealthHud4");
				Destroy(gameObject);
				break;
			case 1:
				UIvida.sprite = Resources.Load<Sprite> ("Sprites/UI/HealthHud3");
				break;
			case 2:
				UIvida.sprite = Resources.Load<Sprite> ("Sprites/UI/HealthHud2");
				break;
			case 3:
				UIvida.sprite = Resources.Load<Sprite> ("Sprites/UI/HealthHud1");
				break;
			default:
				break;
		}
	}

	public void DefaultPushback(Vector2 dir)
	{
		this.rigidbody.AddForce(dir * 10, ForceMode2D.Impulse);
	}
}
