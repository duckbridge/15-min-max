using UnityEngine;
using System.Collections;

public class YesButton : CSBehaviour {

	private AudioSource buttonSound;

	// Use this for initialization
	void Awake () {
		buttonSound = this.transform.Find("ButtonSound").GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnPauseMenuShown() {

	}

	public override void OnPauseGame() {}
	public override void OnResumeGame() {}

	public void OnClicked(RaycastHit hitSummary) {
		this.collider.enabled = false;
		buttonSound.Play();
		Invoke("DoLateDispatch", .2f);
	}

	private void DoLateDispatch() {
		DispatchMessage("OnYesButtonPressed", null);	
	}
}
