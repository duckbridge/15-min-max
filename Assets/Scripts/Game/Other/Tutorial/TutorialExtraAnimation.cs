using UnityEngine;
using System.Collections;

public class TutorialExtraAnimation : CSBehaviour {

	private TextBoxManager textBoxManager;
	// Use this for initialization
	void Start () {
		textBoxManager = this.GetComponentInChildren<TextBoxManager>();
		textBoxManager.AddEventListener(this.gameObject);
		textBoxManager.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D col) {
    	PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
    	if (player) {
    		collider2D.enabled = false;
			textBoxManager.active = true;
			textBoxManager.AddEventListener(player.gameObject);
			textBoxManager.OnActivate();
			DispatchMessage("OnStartTutorialWithTextBox", null);
        }
    }

	public void OnTextDone() {
		DispatchMessage("ContinueGame", null);
		textBoxManager.active = false;
	}
}
