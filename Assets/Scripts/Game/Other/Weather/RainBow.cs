using UnityEngine;
using System.Collections;

public class RainBow : CSBehaviour {

	public float animationPauseTime = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartAnimation() {
		this.animation.Play();
	}

	public void OnPauseAnimation() {
		this.animation["Rainbow animation"].speed = 0f;
		Invoke("ResumeAnimation", animationPauseTime);
	}

	private void ResumeAnimation() {
		this.animation["Rainbow animation"].speed = 1f;
	}

	public void OnAnimationDone() {
		DispatchMessage("OnRainBowDone", null);
	}
}
