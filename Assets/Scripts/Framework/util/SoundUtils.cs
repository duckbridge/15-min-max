using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundUtils : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void MuteAll() {
		List<SoundObject> sounds = SceneUtils.FindObjectsOf<SoundObject>();
		foreach(SoundObject sound in sounds) {
			sound.Mute();
		}
	}

	public static void UnMuteAll() {
		List<SoundObject> sounds = SceneUtils.FindObjectsOf<SoundObject>();
		foreach(SoundObject sound in sounds) {
			sound.UnMute();
		}
	}

	public static void MuteAll(SoundType soundType) {
		List<SoundObject> sounds = SceneUtils.FindObjectsOf<SoundObject>();
		foreach(SoundObject sound in sounds) {
			if(sound.soundType == soundType) {
				sound.Mute();
			}
		}
	}

	public static void UnMuteAll(SoundType soundType) {
		List<SoundObject> sounds = SceneUtils.FindObjectsOf<SoundObject>();
		foreach(SoundObject sound in sounds) {
			if(sound.soundType == soundType) {
				sound.UnMute();
			}
		}
	}
}
