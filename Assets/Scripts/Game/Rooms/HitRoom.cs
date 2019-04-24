using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitRoom : Room {

	private ThrowEnemy throwEnemy;
	public int amountOfObjectsThrown = 1;
	private int amountOfObjectsHit = 0;

	protected override void DoExtraOnEntered() {
		throwEnemy = this.GetComponentInChildren<ThrowEnemy>();
		throwEnemy.AddEventListener(this.gameObject);
		throwEnemy.SetAmountOfObjectsToThrow(amountOfObjectsThrown);
	}

	public void OnBallHit() {
		amountOfObjectsHit++;
		if(amountOfObjectsHit >= amountOfObjectsThrown)
			OpenDoor();
	}

	private void OpenDoor() {
		backdoorTrigger.OpenDoor();
	}
}
