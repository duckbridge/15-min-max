using UnityEngine;
using System.Collections;

public class LogoScreen : MonoBehaviour {

	public float startFadeTimeout = 1f;
	public Fading2D fadeOverlay;

	// Use this for initialization
	void Start () {
		fadeOverlay.AddEventListener(this.gameObject);
		Invoke ("StartFading", startFadeTimeout);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnFadingDone(FadeType fadeType) {
		MaxLoader.LoadIntroScene();
	}
	
	private void StartFading() {
		fadeOverlay.FadeInto(Color.black, 60);
	}
}
