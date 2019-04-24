using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public float destroyTimeout = 2f;
	private AudioSource dropSound;

	// Use this for initialization
	void Start () {
		dropSound = this.transform.Find("BoxDropSound").GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void Destroy() {
		Destroy(this.gameObject);
	}

	private void TurnOnGravity() {
		this.gameObject.AddComponent<Rigidbody2D>();
		this.rigidbody2D.isKinematic = false;
		this.GetComponent<BoxCollider2D>().enabled = true;
	}

	public void OnDrop() {
		TurnOnGravity();
		Invoke("Destroy", destroyTimeout);
	}

	public void PlayDropSound() {
		dropSound.Play();
	}
	
	public SpriteRenderer GetRenderer() {
		return this.transform.Find("BoxBig").GetComponent<SpriteRenderer>();
	}
}
