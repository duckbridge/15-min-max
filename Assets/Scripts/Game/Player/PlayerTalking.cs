using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTalking : MonoBehaviour {

	private List<AudioSource> talkingSounds;
	
	// Use this for initialization
	void Start () {
		talkingSounds = new List<AudioSource>(this.GetComponentsInChildren<AudioSource>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayRandomSound() {
		int randomNumber = Random.Range(0, talkingSounds.Count);
		talkingSounds[randomNumber].Play();
	}
}
