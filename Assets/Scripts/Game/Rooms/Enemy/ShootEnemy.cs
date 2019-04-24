using UnityEngine;
using System.Collections;

public class ShootEnemy : CSBehaviour {

	public int health = 2;
	private AudioSource onHitSound;

	// Use this for initialization
	void Awake () {	
		onHitSound = this.transform.Find("OnHitSound").GetComponent<AudioSource>();
	}

	public void OnHit() {
		health--;
		onHitSound.Play();
		if(health <= 0) {
			Destroy(this.gameObject);
			DispatchMessage("OnShootEnemyDead", this);
		}
	}
}