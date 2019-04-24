using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootRoom : Room {

	private List<ShootEnemy> shootEnemies;
	private int amountOfShootEnemiesKilled = 0;

	private Dude dude;

	protected override void DoExtraOnEntered() {
		shootEnemies = new List<ShootEnemy>(this.GetComponentsInChildren<ShootEnemy>());
		shootEnemies.ForEach(shootEnemy => shootEnemy.AddEventListener(this.gameObject));

		dude = this.GetComponentInChildren<Dude>();
		dude.AddEventListener(this.gameObject);
	}

	public void OnShootEnemyDead(ShootEnemy shootEnemy) {
		amountOfShootEnemiesKilled++;
		
		if(amountOfShootEnemiesKilled >= shootEnemies.Count) {
			OpenDoor();
		}
	}

	public void OnDudeShot(Dude dude) {
		CloseDoor();
	}

	private void OpenDoor() {
		dude.OpenDoor();
		backdoorTrigger.OpenDoor();
	}

	private void CloseDoor() {
		backdoorTrigger.CloseDoor();
		Destroy(dude.gameObject);
	}
}
