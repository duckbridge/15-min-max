using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomTypeManager : MonoBehaviour {

	public Dictionary<RoomType, int> roomScoreByType = new Dictionary<RoomType, int>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnRoomFinished(RoomType roomtype){ 
		int roomtypescore = 0;
		
		roomScoreByType.TryGetValue(roomtype, out roomtypescore);
		if(roomtypescore == 0) {
			roomScoreByType[roomtype] = 1;
		} else {
			roomScoreByType[roomtype] += 1;
		}
	}

	public int GetCurrentScoreForRoomType(RoomType roomtype) {
		int roomtypescore = 0;
		
		roomScoreByType.TryGetValue(roomtype, out roomtypescore);

		return roomtypescore;
	}
}
