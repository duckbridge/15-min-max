using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : CSBehaviour {

	private Transform menuContainer;
	public Transform pauseMenuShowPosition;
	public Transform pauseMenuHidePosition;
	public float menuSpeed = 2f;

	private PauseButton pauseButton;
	private List<PauseMenuButton> pauseMenuButtons;
	
	private enum MenuState { HIDDEN, HIDING, SHOWING, SHOWN }
	private MenuState menuState = MenuState.HIDDEN;

	// Use this for initialization
	void Awake() {
		pauseButton = this.GetComponentInChildren<PauseButton>();
		pauseMenuButtons = new List<PauseMenuButton>(this.GetComponentsInChildren<PauseMenuButton>());
		pauseMenuButtons.ForEach(pauseMenuButton => pauseMenuButton.AddEventListener(this.gameObject));
		pauseButton.AddEventListener(this.gameObject);
		menuContainer = this.transform.Find("MenuContainer");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPauseButtonClicked() {
		switch(menuState) {
			case MenuState.HIDDEN: 
				menuState = MenuState.SHOWING;
				PauseHelper.PauseGame();
				ShowMenu();
				DispatchMessage("OnGamePaused", null);
			break;
			case MenuState.SHOWN:
				menuState = MenuState.HIDING;
				HideMenu();
			break;
		}
	}

	private void HideMenu() {
		iTween.MoveTo(menuContainer.gameObject, iTween.Hash("position", pauseMenuHidePosition.position, "speed", menuSpeed, "oncompletetarget", this.gameObject, "oncomplete", "OnMenuHidden", "easetype", iTween.EaseType.linear));
	}

	private void ShowMenu() {
		iTween.MoveTo(menuContainer.gameObject, iTween.Hash("position", pauseMenuShowPosition.position, "speed", menuSpeed, "oncompletetarget", this.gameObject, "oncomplete", "OnMenuShown", "easetype", iTween.EaseType.linear));
	}

	private void OnMenuShown() {
		menuState = MenuState.SHOWN;
		pauseButton.TogglePauseButton(false);
	}

	private void OnMenuHidden() {
		PauseHelper.ResumeGame();
		menuState = MenuState.HIDDEN;
		pauseButton.TogglePauseButton(true);
		DispatchMessage("OnGameResumed", null);
	}
}
