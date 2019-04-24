using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextBoxManager : CSBehaviour {

	public List<TextBox> textBoxes;
	private int currentTextBox = -1;

	private bool spaceKeyDown = false;
	private bool textBoxIsDone = false;
	public bool activateOnAwake = true;

	public Animation2D lipsAnimation;
	private Animation2D tapScreenAnimation;

	// Use this for initialization
	void Awake () {
		if(activateOnAwake)
			OnActivate();
	}

	public void OnActivate() {
		tapScreenAnimation = this.transform.Find("Tap Screen Animation").GetComponent<Animation2D>();
		lipsAnimation.active = true;
		
		foreach(TextBox textBox in textBoxes) {
			textBox.active = false;
		}
		
		ShowNextTextBalloon();
	}
	
	// Update is called once per frame
	void Update () {
        
		if((Input.GetKey("space") || Input.GetMouseButtonDown(0)) ) {
			if(!spaceKeyDown && textBoxIsDone) {
				spaceKeyDown = true;
				textBoxIsDone = false;
				tapScreenAnimation.Stop();
				tapScreenAnimation.Hide();
				ShowNextTextBalloon();	
			}
		}

		if(spaceKeyDown) {
			if(!Input.GetKey("space")) {
				spaceKeyDown = false;
			}
		}
	}

	public void OnTextDone(TextBox textBox) {
		tapScreenAnimation.Show();
		tapScreenAnimation.Play();

		textBoxIsDone = true;
		Debug.Log("stopping animation?");
		lipsAnimation.Hide();
		lipsAnimation.Pause();

		DispatchMessage("OnTextPartDone", null);

	}

	public void OnShowNextWord() {
		DispatchMessage("OnShowNextWord", null);
	}
	
	private void ShowNextTextBalloon() {
		DispatchMessage("OnShowNextTextBalloon", null);
		if(currentTextBox < textBoxes.Count - 1) {
			currentTextBox++;
			if(currentTextBox - 1 > -1) {
				textBoxes[currentTextBox - 1].RemoveEventListener(this.gameObject);
				textBoxes[currentTextBox - 1].active = false;
			}

			lipsAnimation.Play();

			textBoxes[currentTextBox].AddEventListener(this.gameObject);
			textBoxes[currentTextBox].active = true;
			textBoxes[currentTextBox].OnStart();
		} else {
			Debug.Log("Done!");
			lipsAnimation.Stop();
			lipsAnimation.active = false;
			DispatchMessage("OnTextDone", null);
		}
	}

	public override void OnPauseGame() {}
	public override void OnResumeGame() {}
}
