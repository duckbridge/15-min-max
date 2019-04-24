using UnityEngine;
using System.Collections;

public class Dude : CSBehaviour {

	private Animation2D openDoorAnimation;
	private Animation2D normalAnimation;
	private AudioSource deathSound;
	private AudioSource onHitSound;
	// Use this for initialization
	void Start () {
		normalAnimation = this.transform.Find("NormalAnimation").GetComponent<Animation2D>();
		openDoorAnimation = this.transform.Find("OpenDoorAnimation").GetComponent<Animation2D>();
		deathSound = this.transform.Find("DeathSound").GetComponent<AudioSource>();
		onHitSound = this.transform.Find("OnHitSound").GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnShot() {
		deathSound.Play();
		DispatchMessage("OnDudeShot", this);
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		PlayerControl player = coll.gameObject.GetComponent<PlayerControl>();
		if(player){
			Destroy(rigidbody2D);
			collider2D.enabled = false;
		}
	}

	public void OpenDoor() {
		normalAnimation.Stop();
		normalAnimation.Hide();
		openDoorAnimation.Show();
	}
}
