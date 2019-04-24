using UnityEngine;
using System.Collections;

public class QuitButton : PauseMenuButton {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void OnClicked(RaycastHit hitSummary) {
		SceneUtils.FindObjectOf<ExitHandler>().OnGameExitRequest();
	}
}
