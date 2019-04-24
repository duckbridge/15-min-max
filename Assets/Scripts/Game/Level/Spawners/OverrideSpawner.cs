using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class OverrideSpawner : Spawner {

	public RoomType randomRoomTypeOverride;
	public RoomLevel roomLevelToOverride;
	public bool countUpLevelOnFinish = false;

	public override Room FindRoomForType(RoomType roomType) {
		Room room = null;
		
		room = allRooms.Find(
			_room => roomLevelToOverride ==_room.roomLevel &&
			_room.roomType == roomType
		);

		return room;
	}

	public override void BeforeSpawn() {}

	public override void AfterSpawn() {
		if(countUpLevelOnFinish)
			roomLevelToOverride++;
	}

	protected override RoomType GetRandomRoomType() {
		RoomType randomRoomType = randomRoomTypeOverride;

		return randomRoomType;
	}
}
