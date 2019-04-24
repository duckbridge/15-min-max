using UnityEngine;
using System.Collections;

public class ShootRoomBackDoorTrigger : BackdoorTrigger {

    protected override void OnDoorExit() {
    	DispatchMessage("OnPlayerExittedDoor", this);
    }


    protected override void OnCollide(Collider2D col) {
    	if(doorIsOpen) {
			this.collider2D.enabled = false;
    		OnDoorExit();
		} else {
			PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
			if(player)
				player.OnDie();
		}
    }
}
