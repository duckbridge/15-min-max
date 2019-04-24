using UnityEngine;
using System.Collections;

public class SoundObject : MonoBehaviour {

	public SoundType soundType = SoundType.FX;
	protected float originalVolume;
	protected bool isMuted = false;

	// Use this for initialization
	public virtual void Awake () {
		originalVolume = GetSound().volume;
	}

	public AudioSource GetSound() {
		return this.GetComponent<AudioSource>();
	}

	public void Mute() {
		GetSound().volume = 0f;
		isMuted = true;
	}

	public void UnMute() {
		GetSound().volume = originalVolume;
		isMuted = false;
	}
}
