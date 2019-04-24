using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weather : CSBehaviour {

	public Color targetColor;
	public float fadeTime = 60f;

	public SpriteRenderer spriteToUse;
	protected Color originalColor;

	public int timeAlive = -1;
	public bool usesAnimation = false;
	public WeatherType weatherType;

	private Fading2D fadeComponent;
	private List<Cloud> clouds;
	private bool isInitialized = false;
	public bool fadeClouds = true;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void OnFadingDone(FadeType fadeType) {
		switch(fadeType) {
			case FadeType.FADEIN:
				DoWeather();
			break;

			case FadeType.FADEOUT:
				OnEnd();
			break;
		}
	}

	public virtual void Before() {
		if(fadeClouds)
			clouds.ForEach(cloud => cloud.FadeIn());
		DoFade(targetColor, FadeType.FADEIN);
	}

	protected void DoFade(Color newColor, FadeType fadeType) {
		fadeComponent.FadeInto(newColor, fadeTime, fadeType);
	}

	protected void OnStopWeather() {
		OnEnd();
	}

	public virtual void OnEnd() {
		fadeComponent.RemoveEventListener(this.gameObject);
		DispatchMessage("OnWeatherDone", this);
	}

	public virtual void DoWeather() {
		if(!usesAnimation) {
			Invoke("OnStopWeather", timeAlive);
		}
	}

	public virtual void OnStart() {
			originalColor = spriteToUse.color;
			fadeComponent = this.GetComponent<Fading2D>();
			fadeComponent.SetTarget(spriteToUse);
			fadeComponent.AddEventListener(this.gameObject);
		
		if(!isInitialized) {
			clouds = new List<Cloud>(this.GetComponentsInChildren<Cloud>());
			isInitialized = true;
		}

		Before();
	}

}
