using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Spawner : CSBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.

	public List<Room> allRooms;
	private List<Room> spawnedRooms = new List<Room>();
	public int maxSpawnedObjects = 5;
	
	public virtual void Start() {}

	public virtual Room FindRoomForType(RoomType roomType){ return null; }

	protected virtual RoomType GetRandomRoomType(){ return RoomType.NONE; }

	public virtual void BeforeSpawn() {}

	public virtual void AfterSpawn() {}

	public virtual void OnRoomExitted(RoomType roomType) {}

	public void Spawn () {
		
		RoomType randomRoomType = GetRandomRoomType();
		Room chosenRoom = FindRoomForType(randomRoomType);

		if(!chosenRoom) {
			List<Room> roomsOfChosenType = allRooms.FindAll(_room => _room.roomType == randomRoomType);
			int randomRoomIndex = UnityEngine.Random.Range(0, roomsOfChosenType.Count);
			chosenRoom = roomsOfChosenType[randomRoomIndex];
		}
		
		Room roomToSpawn = (Room) GameObject.Instantiate(chosenRoom, transform.position, transform.rotation);
		spawnedRooms.Add(roomToSpawn);

		if(spawnedRooms.Count >= maxSpawnedObjects) {
			Room objectToBeRemoved = spawnedRooms[0];
			spawnedRooms.RemoveAt(0);
			Destroy(objectToBeRemoved.gameObject);
		}
	}
}
