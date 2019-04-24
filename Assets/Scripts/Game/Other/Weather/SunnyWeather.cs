using UnityEngine;
using System.Collections;

public class SunnyWeather : Weather {

	public override void OnStart() {
		base.OnStart();

		this.animation["SunAnimation"].speed = .1f;
		this.animation["SunAnimation"].time = 0f;
		this.animation.Play("SunAnimation");
	}

	public void OnSunSet() {
		OnStopWeather();
	}
}
