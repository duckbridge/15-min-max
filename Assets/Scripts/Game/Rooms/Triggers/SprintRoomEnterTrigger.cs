using UnityEngine;
using System.Collections;

public class SprintRoomEnterTrigger : BaseRoomTrigger {
    public SprintEnemy sprintEnemy;

    public override void OnEntered() {
        base.OnEntered();
        sprintEnemy.Activate();
    }

    public override void OnPauseGame() {
    	if(sprintEnemy)
    		sprintEnemy.OnPauseGame();
    }

    public override void OnResumeGame() {
    	if(sprintEnemy)
    		sprintEnemy.OnResumeGame();
    }
}
