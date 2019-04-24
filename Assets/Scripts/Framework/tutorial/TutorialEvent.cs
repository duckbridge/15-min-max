using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TutorialEvent : CSBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public abstract void OnEventStarted();

	public abstract void OnEventFinished();
}
