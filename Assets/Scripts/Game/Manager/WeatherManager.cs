using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherManager : CSBehaviour {
	
	private List<Weather> weatherTypes;
	public WeatherType startType;
	
	// Use this for initialization
	void Start () {
		weatherTypes = new List<Weather>(this.GetComponentsInChildren<Weather>());
		foreach(Weather weather in weatherTypes) {
			weather.active = false;
			if(weather.weatherType == startType) {
				weather.active = true;
				weather.OnStart();
				weather.AddEventListener(this.gameObject);

				Invoke("DoLateDispatch", .5f);
			}
		}	

	}
	
	private void DoLateDispatch() {
		DispatchMessage("OnWeatherChanged", startType);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnWeatherDone(Weather weather) {
		weather.RemoveEventListener(this.gameObject);
		weather.active = false; //todo fix
		int randomWeather = Random.Range(0, weatherTypes.Count);
		Weather newWeatherType = weatherTypes[randomWeather];
		newWeatherType.active = true;
		newWeatherType.OnStart();
		newWeatherType.AddEventListener(this.gameObject);
		DispatchMessage("OnWeatherChanged", newWeatherType.weatherType);
	}
}
