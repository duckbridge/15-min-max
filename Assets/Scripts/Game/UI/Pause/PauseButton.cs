using UnityEngine;
using System.Collections;

public class PauseButton : CSBehaviour {

	private SpriteRenderer pauseButton;
	private SpriteRenderer resumeButton;
	private AudioSource buttonSound;

	// Use this for initialization
	void Start () {
		pauseButton = this.transform.Find("pausebutton").GetComponent<SpriteRenderer>();
		resumeButton = this.transform.Find("resumebutton").GetComponent<SpriteRenderer>();
		buttonSound = this.transform.Find("PauseButtonSound").GetComponent<AudioSource>();

		resumeButton.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnPauseMenuShown() {

	}

	public override void OnPauseGame() {}
	public override void OnResumeGame() {}

	public void TogglePauseButton(bool show) {
		pauseButton.active = show;
		resumeButton.active = !show;

	}
	public void OnClicked(RaycastHit hitSummary) {
		buttonSound.Play();
		DispatchMessage("OnPauseButtonClicked", null);
	}
}
