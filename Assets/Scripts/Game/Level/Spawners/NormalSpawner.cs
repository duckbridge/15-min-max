using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class NormalSpawner : Spawner {
	
	private static char seperator = ':';

	public List<RoomType> unlockedRoomTypes;
	private List<RoomType> roomsThatNeedToBeUnlocked;
	private RoomType newlyUnlockedRoomType = RoomType.NONE;

	public int chanceOutofTenThatAllRoomTypesAreSearched = 6;

	public override void Start() {
		LoadUnlockedRoomTypes();
		roomsThatNeedToBeUnlocked = new List<RoomType>(unlockedRoomTypes);
	}

	public override Room FindRoomForType(RoomType roomType) {
		Room room = null;
		
		int currentScoreForRoomType = SceneUtils.FindObjectOf<RoomTypeManager>().GetCurrentScoreForRoomType(roomType);

		room = allRooms.Find(
			_room => currentScoreForRoomType >= _room.minimumScoreRequired && 
			currentScoreForRoomType <= _room.maximumScoreRequired && 
			_room.roomType == roomType
		);

		return room;
	}


	public override void BeforeSpawn() {}

	public override void AfterSpawn() {}

	protected override RoomType GetRandomRoomType() { //maybe modify this in future
		
		int chooseRoomTypeThatStillNeedsToBeUnlocked = UnityEngine.Random.Range(0, 10);
		RoomType randomRoomType = RoomType.NONE;

		if(chooseRoomTypeThatStillNeedsToBeUnlocked < chanceOutofTenThatAllRoomTypesAreSearched) {
			Debug.Log("searching in all room types!");
			int randomRoomTypeIndex = UnityEngine.Random.Range(0, unlockedRoomTypes.Count);
			randomRoomType = unlockedRoomTypes.ToArray()[randomRoomTypeIndex];

		} else if(newlyUnlockedRoomType != RoomType.NONE) {
			Debug.Log("doing new room type!");
			Debug.Log(newlyUnlockedRoomType);
			randomRoomType = newlyUnlockedRoomType;
			newlyUnlockedRoomType = RoomType.NONE;
		}
		
		if(randomRoomType == RoomType.NONE) {
			int randomRoomTypeIndex = UnityEngine.Random.Range(0, unlockedRoomTypes.Count);
			randomRoomType = unlockedRoomTypes.ToArray()[randomRoomTypeIndex];
		}

		return randomRoomType;
	}

	public override void OnRoomExitted(RoomType roomType) {
		if(roomsThatNeedToBeUnlocked.Contains(roomType)) {
			Debug.Log("removing from roomsThatNeedToBeUnlocked");
			roomsThatNeedToBeUnlocked.Remove(roomType);
		}

		if(roomsThatNeedToBeUnlocked.Count == 0) { //add new room
			Debug.Log("adding new room");
			List<RoomType> roomTypesLeft = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().ToList();
			roomTypesLeft.Remove(RoomType.NONE);
			
			foreach(RoomType roomtype in unlockedRoomTypes) {
				roomTypesLeft.Remove(roomtype);
			}

			RoomType randomRoomType;

			if(roomTypesLeft.Count > 0) {
				int randomRoomTypeIndex = UnityEngine.Random.Range(0, roomTypesLeft.Count);
				randomRoomType = roomTypesLeft[randomRoomTypeIndex];
			} else { //no room types left!
				int randomRoomTypeIndex = UnityEngine.Random.Range(0, unlockedRoomTypes.Count);
				randomRoomType = unlockedRoomTypes[randomRoomTypeIndex];	
			}

			if(!unlockedRoomTypes.Contains(randomRoomType)) {
				newlyUnlockedRoomType = randomRoomType;
				
				unlockedRoomTypes.Add(randomRoomType);
				roomsThatNeedToBeUnlocked.Add(randomRoomType);
				
				Debug.Log("new : " + randomRoomType);

				SaveUnlockedRoomTypes();
			}
		}
	}

	private void LoadUnlockedRoomTypes() {
		string saveString = PlayerPrefs.GetString(GlobalSaveVars.UNLOCKEDROOMS, "");
		Debug.Log(saveString);
		if(saveString.Length > 0) {
			string[] splittedString = saveString.Split(seperator);
			foreach(string splitted in splittedString) {
				try {
					RoomType roomType = (RoomType) Enum.Parse(typeof(RoomType), splitted);
					if(!unlockedRoomTypes.Contains(roomType)) {
						unlockedRoomTypes.Add(roomType);
					}
				} catch (ArgumentException) { 
					PlayerPrefs.SetString(GlobalSaveVars.UNLOCKEDROOMS, "");
					PlayerPrefs.Save();
					Debug.Log("corrupt save file!");
					
					return;
				}
			}
		}
	}

	private void SaveUnlockedRoomTypes() {
		string saveString = "";
		
		for(int i = 0 ; i < unlockedRoomTypes.Count ; i++) {
			saveString += unlockedRoomTypes[i].ToString();

			if(i != unlockedRoomTypes.Count - 1) {
				saveString += seperator;
			}
		}

		PlayerPrefs.SetString(GlobalSaveVars.UNLOCKEDROOMS, saveString);	
		PlayerPrefs.Save();
	}
}
