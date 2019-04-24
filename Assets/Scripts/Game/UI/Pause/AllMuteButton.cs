using UnityEngine;
using System.Collections;

public class AllMuteButton : PauseMenuButton {

	private Transform allMuteTransform;
	private Transform allUnmuteTransform;

	private bool soundMuted = false;

	private AudioSource buttonSound;

	// Use this for initialization
	void Start () {
		buttonSound = this.transform.Find("ButtonSound").GetComponent<AudioSource>();

		allMuteTransform = this.transform.Find("ALLMute");
		allUnmuteTransform = this.transform.Find("ALLUnMute");

		int savedMuted = PlayerPrefs.GetInt(BGMuteButton.saveName, 0);
		if(savedMuted == 1) {
			soundMuted = true;
		} else {
			soundMuted = false;
		}

		DoMute();
	}

	public override void OnClicked(RaycastHit hitSummary) {
		soundMuted = !soundMuted;
		DoMute();
		buttonSound.Play();
	}

	private void DoMute() {
		allUnmuteTransform.active = soundMuted;
		allMuteTransform.active = !soundMuted;

		if(soundMuted) {
			PlayerPrefs.SetInt(BGMuteButton.saveName, 1);
			PlayerPrefs.SetInt(FXMuteButton.saveName, 1);
			SoundUtils.MuteAll();
		} else {
			PlayerPrefs.SetInt(BGMuteButton.saveName, 0);
			PlayerPrefs.SetInt(FXMuteButton.saveName, 0);
			SoundUtils.UnMuteAll();
		}

		PlayerPrefs.Save();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
