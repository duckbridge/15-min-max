using UnityEngine;
using System.Collections;

public class DropRoom : Room {
	public int boxesCarriedByPlayer = 1;

	protected override void DoExtraOnEntered() {
		player.CarryBoxes(boxesCarriedByPlayer);
	}
}
