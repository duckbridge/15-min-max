using UnityEngine;
using System.Collections;

public class DropRoomBackDoorTrigger : BackdoorTrigger {

    protected override void OnDoorExit() {
    	Debug.Log("onplayerexit");
    	DispatchMessage("OnPlayerExittedDoor", this);
    }
}
