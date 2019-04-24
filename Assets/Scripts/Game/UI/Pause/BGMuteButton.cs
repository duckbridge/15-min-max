using UnityEngine;
using System.Collections;

public class BGMuteButton : PauseMenuButton {

	private Transform bgMuteTransform;
	private Transform bgUnmuteTransform;

	private bool soundMuted = false;
	public static string saveName = "BGMMUTED";

	private AudioSource buttonSound;

	// Use this for initialization
	void Start () {
		buttonSound = this.transform.Find("ButtonSound").GetComponent<AudioSource>();

		bgMuteTransform = this.transform.Find("BGMute");
		bgUnmuteTransform = this.transform.Find("BGUnMute");

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
		DoMute();
		buttonSound.Play();
	}

	private void DoMute() {
		bgUnmuteTransform.active = soundMuted;
		bgMuteTransform.active = !soundMuted;

		if(soundMuted) {
			PlayerPrefs.SetInt(saveName, 1);
			SoundUtils.MuteAll(SoundType.BG);
		} else {
			PlayerPrefs.SetInt(saveName, 0);
			SoundUtils.UnMuteAll(SoundType.BG);
		}

		PlayerPrefs.Save();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
