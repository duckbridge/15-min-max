using UnityEngine;
using System.Collections;

public class FadingAudio : SoundObject {
	
	private AudioSource audio;

	private bool isFadingOut = false;
	private bool isFadingIn = false;
	private float fadeSpeed = 0f;

	// Use this for initialization
	public override void Awake () {
		base.Awake ();
		audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isFadingOut) {
			audio.volume -= fadeSpeed;
			if(audio.volume <= 0f) {
				isFadingOut = false;
				Stop();
			}
		}

		if(isFadingIn) {
			audio.volume += fadeSpeed;
			if(audio.volume >= 1f) {
				isFadingIn = false;
			}
		}
	}

	public void PlayAtTime(float time) {
		audio.time = time;
		audio.Play();
	}

	public void FadeOut(float speed) {
		if(!isMuted) {
			audio.volume = originalVolume;
			isFadingOut = true;
			fadeSpeed = speed;
		}
	}

	public void FadeIn(float speed) {
		if(!isMuted) {
			audio.volume = 0f;
			isFadingIn = true;
			fadeSpeed = speed;
		}
	}

	public float GetCurrentPlayTime() {
		return audio.time;
	}

	public void Stop() {
		audio.Stop();
	}

	public void Play() {
		audio.Play();
	}
}
