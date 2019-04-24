using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PersistedStorage : MonoBehaviour {
	private static string LASTROOMSAVENAME = "LASTROOMSAV";

	public void Awake() {}

	public void Update() {

	}

	public void SetLastPlayedRoomType(RoomType roomType) {
		Debug.Log("saving," + roomType.ToString());
		PlayerPrefs.SetString(LASTROOMSAVENAME, roomType.ToString());
		PlayerPrefs.Save();
	}

	public RoomType GetLastPlayedRoomType() {
		RoomType lastRoomType = (RoomType) Enum.Parse(typeof(RoomType), PlayerPrefs.GetString(LASTROOMSAVENAME ,"NONE"));
		return lastRoomType;
	}

	public void ResetRoomType() {
		Debug.Log("reset");
		PlayerPrefs.SetString(LASTROOMSAVENAME, RoomType.NONE.ToString());
		PlayerPrefs.Save();
	}
}
