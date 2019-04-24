using UnityEngine;
using System.Collections;

public class DarkWeather : Weather {

	private GameObject fallingStar;
	public float fallingStarTimeout = 2f;

	public override void OnStart() {
		base.OnStart();
		
		fallingStar = this.transform.Find("fallingStar").gameObject;

		this.animation["MoonAnimation"].speed = .1f;
		this.animation["MoonAnimation"].time = 0f;
		this.animation.Play("MoonAnimation");

		Invoke("DoFallingStarAnimation", fallingStarTimeout);
	}

	private void DoFallingStarAnimation() {
		fallingStar.animation.Play("FallingStarAnimation");
	}

	public void OnMoonDone() {
		OnStopWeather();
	}
}
