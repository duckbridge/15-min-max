using UnityEngine;
using System.Collections;

public class MenuButton : CSBehaviour {

	public MenuButtonType menuButtonType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void OnPressed() {
		DispatchMessage("OnMenuButtonPressed", this.menuButtonType);
	}

	public virtual void OnObjectClicked() {
		DispatchMessage("OnMenuButtonPressed", this.menuButtonType);
	}

	public virtual void OnSelected() {}
	public virtual void OnUnSelected() {}
}
