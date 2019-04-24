using UnityEngine;
using System.Collections;

public class FXMuteButton : PauseMenuButton {

	private Transform muteTransform;
	private Transform unMuteTransform;

	private bool soundMuted = false;
	public static string saveName = "FXMMUTED";

	private AudioSource buttonSound;


	// Use this for initialization
	void Start () {
		buttonSound = this.transform.Find("ButtonSound").GetComponent<AudioSource>();

		muteTransform = this.transform.Find("FXMute");
		unMuteTransform = this.transform.Find("FXUnmute");

		int savedMuted = PlayerPrefs.GetInt(saveName, 0);
		if(savedMuted == 1) {
			soundMuted = true;
		} else {
			soundMuted = false;
		}

		DoMute();
	}

	public override void OnClicked(RaycastHit hitSummary) {
		soundMuted = !soundMuted;
		Debug.Log("soundMuted " + soundMuted);
		DoMute();
		buttonSound.Play();
	
	}

	private void DoMute() {
		unMuteTransform.active = soundMuted;
		muteTransform.active = !soundMuted;

		if(soundMuted) {
			PlayerPrefs.SetInt(saveName, 1);
			SoundUtils.MuteAll(SoundType.FX);
		} else {
			PlayerPrefs.SetInt(saveName, 0);
			SoundUtils.UnMuteAll(SoundType.FX);
		}

		PlayerPrefs.Save();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
