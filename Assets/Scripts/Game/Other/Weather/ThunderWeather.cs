using UnityEngine;
using System.Collections;

public class ThunderWeather : Weather {

	private ThunderCloud[] thunderClouds;

	public override void OnEnd() {
		foreach(ThunderCloud thunderCloud in thunderClouds) {
			thunderCloud.StopThunder();
		}
		base.OnEnd();
	}

	public override void OnStart() {
		base.OnStart();
		thunderClouds = this.GetComponentsInChildren<ThunderCloud>();
		foreach(ThunderCloud thunderCloud in thunderClouds) {
			thunderCloud.active = true;
		}
	}

	public void OnMoonDone() {
		OnStopWeather();
	}
}
