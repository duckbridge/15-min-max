using UnityEngine;
using System.Collections;

public class SkipTutorialButton : CSBehaviour {

	private AudioSource buttonSound;

	// Use this for initialization
	void Start () {
		buttonSound = this.transform.Find("ButtonSound").GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClicked(RaycastHit hit) {
		this.collider.enabled = false;
		buttonSound.Play();
		DispatchMessage("OnSkipTutorial", null);
	}
}
