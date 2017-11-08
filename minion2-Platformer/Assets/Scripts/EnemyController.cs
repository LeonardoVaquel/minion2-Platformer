using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Rigidbody2D rigidbody;
	public AudioSource audio;
	public float speed;
	public float maxSpeed;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
		audio = GetComponent<AudioSource> ();
		speed = 5f;
		maxSpeed = 5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.rigidbody.AddForce (Vector2.left * speed);

		float limitedSpeed = Mathf.Clamp (this.rigidbody.velocity.x, -maxSpeed, maxSpeed);
		this.rigidbody.velocity = new Vector2 (limitedSpeed, this.rigidbody.velocity.y);

		if (rigidbody.velocity.x > -0.01f && rigidbody.velocity.x < 0.01f) {
			speed = -speed;
			this.rigidbody.velocity = new Vector2 (speed, this.rigidbody.velocity.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			audio.Play ();
			Destroy (gameObject);
		}
	}
}
