using UnityEngine;
using System.Collections;

public class ThrowableObject : CSBehaviour {

	private SoundObject onHitSound;

	private float speed;
	private float savedSpeed;

	private float directionX = 1;
	private float directionY = 0;

	private bool isHitBybat = false;

	public float hitByPlayerMultiplier = 1.5f;
	public float rotationSpeed = 100f;

	private float minimumYOffset = 0;
	private float maximumYOffset = 5;
	public float destroyTimeout = 3f;

	// Use this for initialization

	void Start () {
		onHitSound = this.GetComponent<SoundObject>();
	}

	public void ThrowToLeft(float speed) {
		this.speed = speed;
		this.directionX = -1;
	}

	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = new Vector3(this.transform.position.x + (directionX*speed), this.transform.position.y + (directionY*speed), this.transform.position.z);
		this.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
	}

	void OnTriggerEnter2D(Collider2D col) {
		PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
		ThrowEnemy throwEnemy = col.gameObject.GetComponent<ThrowEnemy>();
		PlayerBat bat = col.gameObject.GetComponent<PlayerBat>();

		if(bat) {
			this.collider2D.enabled = false;
			DispatchMessage("OnHitByBat", this);
			this.directionX = 1;
			this.directionY = Random.Range(minimumYOffset, maximumYOffset);
			this.speed *= hitByPlayerMultiplier;
			isHitBybat = true;
			onHitSound.GetSound().Play();
			Invoke("OnDestroy", destroyTimeout);
			return;
		}

		if(throwEnemy && isHitBybat) {
			DispatchMessage("OnHitByThrowable", this);
			Destroy(this.gameObject);
		}

		if(player) {
			this.collider2D.enabled = false;
			player.OnDie();
		}
	}

	public void OnGamePaused() {
		savedSpeed = speed;
		speed = 0f;
	}

	public void OnGameResumed() {
		speed = savedSpeed;
	}

	private void OnDestroy() {
		Destroy(this.gameObject);
	}
}
