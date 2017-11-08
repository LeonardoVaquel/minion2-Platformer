using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainInteraction : MonoBehaviour {

	public AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			audio.Play();
		}
	}
}
