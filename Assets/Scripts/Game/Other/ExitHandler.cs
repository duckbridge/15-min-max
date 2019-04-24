using UnityEngine;
using System.Collections;

public class ExitHandler : CSBehaviour {

	private HardwareButtonsHandler hardwareButtonHandler;
	private Transform exitButtonsContainer;
	public bool isEndScene = false;

	private Fading2D fadingBackground;
	private Color originalColor;

	// Use this for initialization
	void Start () {
		hardwareButtonHandler = this.GetComponentInChildren<HardwareButtonsHandler>();
		hardwareButtonHandler.AddEventListener(this.gameObject);
		exitButtonsContainer = this.transform.Find("ExitButtonsContainer");

		fadingBackground = exitButtonsContainer.transform.Find("Background").GetComponent<Fading2D>();
		originalColor = fadingBackground.GetComponent<SpriteRenderer>().color;

		exitButtonsContainer.GetComponentInChildren<YesButton>().AddEventListener(this.gameObject);
		exitButtonsContainer.GetComponentInChildren<NoButton>().AddEventListener(this.gameObject);
		exitButtonsContainer.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnGameExitRequest() {
		CancelInvoke("HideContainer");
		PauseHelper.PauseGame();
		exitButtonsContainer.active = true;
		fadingBackground.FadeInto(Color.black, 10);
	}

	public void OnNoButtonPressed() {
		PauseHelper.ResumeGame();
		Invoke ("HideContainer", .1f);
		fadingBackground.FadeInto(originalColor, 10);
	}

	private void HideContainer() {
		CancelInvoke("HideContainer");
		exitButtonsContainer.active = false;
	}

	public void OnYesButtonPressed() {
		if(isEndScene) {
			DispatchMessage("OnQuitRequested", null);
		} else {
			Application.Quit();
		}
	}
}
