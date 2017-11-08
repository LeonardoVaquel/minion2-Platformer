using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithEnemy : MonoBehaviour {
	public Collider2D collider;
	public AudioSource sonidoMatarEnemigo;
	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider2D> ();
		sonidoMatarEnemigo = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnColliderStay2D(Collider2D col){
		if (col.gameObject.tag == "enemigoHead") {
			Debug.Log ("KLASDASDASDASDASDAS");
			Destroy (col.gameObject);
			sonidoMatarEnemigo.Play ();
		}
	}
}
