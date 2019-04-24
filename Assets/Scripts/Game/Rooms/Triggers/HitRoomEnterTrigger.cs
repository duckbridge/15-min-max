using UnityEngine;
using System.Collections;

public class HitRoomEnterTrigger : BaseRoomTrigger {

	public ThrowEnemy throwEnemy;
	
    public override void OnEntered() {
        base.OnEntered();
        throwEnemy.OnThrow();
    }
}
