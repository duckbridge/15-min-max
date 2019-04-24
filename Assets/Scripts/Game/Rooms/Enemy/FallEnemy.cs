using UnityEngine;
using System.Collections;

public class FallEnemy : CSBehaviour {

	private float aliveTimer = 3000f;

	private bool fallingIsPaused = false;
	private bool fallRequested = false;
	// Use this for initialization
	void Awake () {	}

	public void RequestFall() {
		fallRequested = true;
		DoFall();
	}

	private void DoFall() {
		if(!fallingIsPaused) {
			this.rigidbody2D.isKinematic = false;
			GetComponent<Animation2D>().Play();
		}
	}

	public void FixedUpdate() {
		if(!fallingIsPaused) {
			aliveTimer -= 1f;

			if(aliveTimer <= 0f) {
				Destroy(this.gameObject);
			}
		}
	}

	public override void OnPauseGame() {
		fallingIsPaused = true;
		this.rigidbody2D.isKinematic = true;
	}

	public override void OnResumeGame() {
		fallingIsPaused = false;

		if(fallRequested) {
			DoFall();
		}
	}

}