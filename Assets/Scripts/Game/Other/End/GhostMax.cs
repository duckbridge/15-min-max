using UnityEngine;
using System.Collections;

public class GhostMax : MonoBehaviour {

	private PlayerTalking talking;
	private TextBoxManager textBoxManager;

	// Use this for initialization
	void Start () {
		talking = this.GetComponentInChildren<PlayerTalking>();
		textBoxManager = SceneUtils.FindObjectOf<TextBoxManager>();
		textBoxManager.AddEventListener(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTextDone() {
		textBoxManager.active = false;
	}

	public void OnShowNextWord() {
		talking.PlayRandomSound();
	}
}
