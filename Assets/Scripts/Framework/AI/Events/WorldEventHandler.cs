using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldEventHandler : CSBehaviour {

	// Use this for initialization
	void Start () {
		SceneUtils.FindObjectOf<WorldEventManager>().AddEventListener(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UnSubScribe() {
		SceneUtils.FindObjectOf<WorldEventManager>().RemoveEventListener(this.gameObject);
	}

	public void OnWorldStateChanged(List<WorldState> newWorldState) {
		DispatchMessage("OnWorldStateChanged", newWorldState);
	}
}
