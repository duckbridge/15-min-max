using UnityEngine;
using System.Collections;

public class PauseMenuButton : CSBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void OnClicked(RaycastHit hitSummary) {
		Debug.Log("HIT");
	}
}
