using UnityEngine;
using System.Collections;

public class RainbowWeather : Weather {

	private RainBow rainBow;

	public override void OnEnd() {
		base.OnEnd();
	}

	public override void OnStart() {
		base.OnStart();
		rainBow = this.GetComponentInChildren<RainBow>();
		rainBow.AddEventListener(this.gameObject);
		rainBow.StartAnimation();
	}

	public void OnRainBowDone() {
		OnStopWeather();
	}
}
