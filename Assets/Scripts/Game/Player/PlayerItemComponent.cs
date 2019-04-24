using UnityEngine;
using System.Collections;

public class PlayerItemComponent : MonoBehaviour {

	private WeatherManager weatherManager;
    private bool isInside = false;
    
    private PlayerItem currentItem, queuedItem;
	public PlayerItem umbrella;
    
	void Start () {
		weatherManager = SceneUtils.FindObjectOf<WeatherManager>();
		if(weatherManager)
        	weatherManager.AddEventListener(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEnterRoom(RoomType roomType) {
 		isInside = true;
 		if(currentItem)
 			currentItem.active = false;
 		if(queuedItem)
 			queuedItem.active = false;

	}

	public void OnRoomExitted(RoomType roomType) {
		isInside = false;
		SwitchItem();
	}

	private void SwitchItem() {
		if(currentItem)
			currentItem.active = true;
		else if(queuedItem) {
			queuedItem.active = true;
		}
	}

	public void OnWeatherChanged(WeatherType weatherType) { //move to component

		ResetItems();
        switch(weatherType) {
        	case WeatherType.THUNDER:
            case WeatherType.RAIN:
                if(isInside) {
                	queuedItem = umbrella;
                } else {
                    currentItem = umbrella;
                    SwitchItem();
                }
            break;
        }
    }

    private void ResetItems() {
    	if(currentItem)
			currentItem.active = false;
		if(queuedItem)
			queuedItem.active = false;
		queuedItem = null;
		currentItem = null;
    }

    public void Disable() {
    	umbrella.renderer.enabled = false;
    }

    public void Enable() {
    	umbrella.renderer.enabled = true;
    }
}
