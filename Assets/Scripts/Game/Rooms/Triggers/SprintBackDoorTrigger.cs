using UnityEngine;
using System.Collections;

public class SprintBackDoorTrigger : BackdoorTrigger
{
    public SprintEnemy sprintEnemy;
    
    protected override void OnDoorExit() {
       base.OnDoorExit();
       sprintEnemy.OnPlayerGotAway();
    }
}
