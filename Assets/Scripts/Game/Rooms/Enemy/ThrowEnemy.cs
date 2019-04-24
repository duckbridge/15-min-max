using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowEnemy : CSBehaviour {

	private Animation2D openDoorAnimation;
	private Animation2D throwAnimation;

	public float minimalThrowSpeed = 50f;
	public float maximumThrowSpeed = 80f;
	
	public List<ThrowableObject> throwables;
	private List<ThrowableObject> throwablesLeft;

	private ThrowableObject throwable;
	private Transform throwPoint;
	private Transform killTrigger;

	private int amountOfObjectsToThrow;
	private int currentThrowIndex = 0;
	private int amountOfObjectsHitByPlayer = 0;

	public float objectThrowTimeout = 1f;

	// Use this for initialization
	void Awake () {
		throwablesLeft = new List<ThrowableObject>(throwables);
		throwAnimation = this.transform.Find("Throw Animation").GetComponent<Animation2D>();
		openDoorAnimation = this.transform.Find("OpenDoorAnimation").GetComponent<Animation2D>();
		killTrigger = this.transform.Find("killTrigger");
		throwAnimation.AddEventListener(this.gameObject);
	
		throwPoint = this.transform.Find("ThrowPoint");	
	}
	
	public void OnAnimationDone(Animation2D animation2d) {
		throwable.active = true;
		throwable.transform.parent = null;
		float throwSpeed = Random.Range(minimalThrowSpeed, maximumThrowSpeed);
		throwable.ThrowToLeft(throwSpeed);
		throwable.AddEventListener(this.gameObject);
	}

	public void OnThrow() {
		int randomThrowableIndex = Random.Range(0, throwablesLeft.Count);
		throwable = throwablesLeft[randomThrowableIndex];
		throwablesLeft.Remove(throwable);
		throwable.transform.position = throwPoint.position;	

		currentThrowIndex++;
		throwAnimation.Play(true);
		if(currentThrowIndex < amountOfObjectsToThrow) {
			Invoke("OnThrow", objectThrowTimeout);
		}
	}

	public void OnHitByBat(ThrowableObject throwableObj) {
		amountOfObjectsHitByPlayer++;
		
		if(amountOfObjectsHitByPlayer >= amountOfObjectsToThrow) {
			OnPlaySadAnimation();
			Destroy(rigidbody2D);

			Collider2D[] colliders2D = GetComponents<Collider2D>();
			for(int i = 0; i < colliders2D.Length ;i++) {
				colliders2D[i].enabled = false;
			}

			killTrigger.collider2D.enabled = false;
		}

		DispatchMessage("OnBallHit", null);
	}

	public void OnHitByThrowable(ThrowableObject throwableObj) {}

	// Update is called once per frame
	void Update () {
	}

	public void OnPlaySadAnimation() {
		throwAnimation.Stop();
		throwAnimation.Hide();
		openDoorAnimation.Show();
		openDoorAnimation.Play();

		//clear otherwise pausing the game will break the game
		throwables.Clear();
	}

	private void OnDestroy() {
		Destroy(this.gameObject);
	}

	public override void OnPauseGame() {
		throwables.ForEach(throwable => throwable.OnPauseGame());
	}

	public override void OnResumeGame() {
		throwables.ForEach(throwable => throwable.OnResumeGame());
	}

	public void SetAmountOfObjectsToThrow(int amountOfObjectsToThrow) {
		this.amountOfObjectsToThrow = amountOfObjectsToThrow;
	}
}
