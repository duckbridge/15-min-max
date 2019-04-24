using UnityEngine;
using System.Collections;

public class PlayMusicButton : MonoBehaviour {

	public bool playOnStart = false;
	private AudioSource music;
	private bool isPlaying = false;

	private SpriteRenderer playButton;
	private SpriteRenderer pauseButton;

	// Use this for initialization
	void Awake () {
		playButton = this.transform.Find ("PlayButton").GetComponent<SpriteRenderer>();
		pauseButton = this.transform.Find ("PauseButton").GetComponent<SpriteRenderer>();

		playButton.active = !playOnStart;
		pauseButton.active = playOnStart;
		isPlaying = playOnStart;

		music = this.GetComponentInChildren<AudioSource>();
		if(playOnStart) {
			music.Play();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClicked(RaycastHit raycastHit) {
		if(isPlaying) {
			music.Pause();
			isPlaying = false;
		} else {
			music.Play();
			isPlaying = true;
		}

		playButton.active = !isPlaying;
		pauseButton.active = isPlaying;

	}
}
