using UnityEngine;
using System.Collections;

public class DropRoomEnterTrigger : BaseRoomTrigger
{
    PlayerControl control;
    public DropManager dropManager;
    
    public override void OnEntered() {
        base.OnEntered();
		dropManager.OnActivate();        
    }
}
