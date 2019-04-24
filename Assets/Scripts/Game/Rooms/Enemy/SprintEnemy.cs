using UnityEngine;
using System.Collections;

public class SprintEnemy : CSBehaviour {

	public float movementSpeed = 1f;
	public Animation2D sadAnimation;
	public Animation2D runAnimation;
	private bool canMove = true;
	public BoxCollider2D normalCollider, triggerCollider;
	// Use this for initialization
	void Awake () {
	}
	
	public override void OnPauseGame() {
		this.canMove = false;
		runAnimation.Pause();
		this.rigidbody2D.velocity = Vector2.zero;
	}

	public override void OnResumeGame() {
		this.canMove = true;
		if(isEnabled)
			runAnimation.Play();
	}
	// Update is called once per frame
	void Update () {
		if(canMove && isEnabled) {
			this.rigidbody2D.velocity = new Vector3(movementSpeed, this.rigidbody2D.velocity.y);
		}
	}

	public void Activate() {
		this.active = true;
		this.isEnabled = true;
		runAnimation.Show();
		runAnimation.Play(true);
	}

	public void OnPlayerGotAway() {
		canMove = false;
		this.isEnabled = false;
		runAnimation.Stop();
		runAnimation.Hide();
		sadAnimation.Play();
		sadAnimation.Show();
		
		this.rigidbody2D.velocity = new Vector2(0f,0f);
		this.rigidbody2D.isKinematic = true;
		this.triggerCollider.enabled = false;
		this.normalCollider.enabled = false;
		Invoke("OnDestroy", 5f);
	}

	void OnTriggerEnter2D(Collider2D col) {
		PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
		if(player) {
			player.OnDie();
			OnPlayerGotAway();
		}
	}

	private void OnDestroy() {
		Destroy(this.gameObject);
	}
}
