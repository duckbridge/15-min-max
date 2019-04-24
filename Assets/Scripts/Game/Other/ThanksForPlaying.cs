using UnityEngine;
using System.Collections;

public class ThanksForPlaying : MonoBehaviour {

	public float quitGameTimeout = 5f;
	public Fading2D fadeOverlay;

	// Use this for initialization
	void Start () {
		fadeOverlay.AddEventListener(this.gameObject);
		Invoke ("StartFading", quitGameTimeout);
	}


	public void OnFadingDone(FadeType fadeType) {
		Application.Quit();
	}

	private void StartFading() {
		fadeOverlay.FadeInto(Color.black, 120);
	}


	// Update is called once per frame
	void Update () {
	
	}
}
