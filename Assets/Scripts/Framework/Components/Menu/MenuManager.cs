using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

	private List<MenuButton> menuButtons;
	private bool isActive = true;

	private MenuButton currentMenuButton;
	private int currentIndex = 0;
	public Scene sceneAfterStartPressed;

	// Use this for initialization
	void Start () {
		menuButtons = new List<MenuButton>(this.GetComponentsInChildren<MenuButton>());
		menuButtons.ForEach(button => button.AddEventListener(this.gameObject));
		currentMenuButton = menuButtons[0];
		currentMenuButton.OnSelected();
	}
	

	private void OnMoveToPreviousButton() {
		menuButtons[currentIndex].OnUnSelected();
		--currentIndex;
		if(currentIndex < 0) {
			currentIndex = menuButtons.Count-1;
		}
		menuButtons[currentIndex].OnSelected();
	}

	private void OnMoveToNextButton() {
		menuButtons[currentIndex].OnUnSelected();
		++currentIndex;
		if(currentIndex >= menuButtons.Count) {
			currentIndex = 0;
		}
		menuButtons[currentIndex].OnSelected();
	}

	public void OnMenuButtonPressed(MenuButtonType menuButtonType) {
		if(!this.isActive) {
			return;
		}

		switch(menuButtonType) {
			case MenuButtonType.EXIT:
				Application.Quit();
			break;
			case MenuButtonType.STARTGAME:
				//go to startgame
			break;
			case MenuButtonType.LOADINTRO:
				//go to load intro
			break;
		}
		this.isActive = false;
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
			OnMoveToPreviousButton();
		}

		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
			OnMoveToNextButton();
		}

		if(Input.GetKeyDown(KeyCode.Return)) {
			menuButtons[currentIndex].OnPressed();
		}
	}
}
