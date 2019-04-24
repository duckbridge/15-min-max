using UnityEngine;
using System.Collections;

public class MaxGhost : CSBehaviour {

	public float animationDelay = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FlyUp() {
		Invoke("PlayAnimationDelayed", animationDelay);
	}

	private void PlayAnimationDelayed() {
		this.animation.Play();
	}

	public void OnFlewUp() {
		DispatchMessage("OnGhostFlyingDone", null);
	}
}
