using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TutorialManager : GameManager {

	private List<Room> rooms;
	public SpriteRenderer output;
	private Transform tutorialAnimations;

	private bool isInTutorialMode = false;
	private RoomType savedRoomType = RoomType.NONE;
	private Animation2D animationForCurrentRoomType;
	private OnTutorialDoneTrigger onTutorialDoneTrigger;
	private List<TutorialExtraAnimation> tutorialExtraAnimations;
	
	private List<string> unlockedAnimations;
	private RoomType lastEnteredRoomType = RoomType.NONE;
	
	private static char seperator = ':';

	private bool tutorialDone = false;
	private bool tutorialUsesTextBox = false;

	public override void Start () {

		LoadTutorialsSeen();
	
		tutorialAnimations = this.transform.Find("TutorialAnimations");
		tutorialExtraAnimations = new List<TutorialExtraAnimation>(this.GetComponentsInChildren<TutorialExtraAnimation>());
		tutorialExtraAnimations.ForEach(tutorialExtraAnimation => tutorialExtraAnimation.AddEventListener(this.gameObject));
		
		onTutorialDoneTrigger = this.transform.Find("OnTutorialDoneTrigger").GetComponent<OnTutorialDoneTrigger>();
		onTutorialDoneTrigger.AddEventListener(this.gameObject);
		
		hills = new List<ITweenMovableObject>(
			this.transform.Find("mainCamera/Hills").GetComponentsInChildren<ITweenMovableObject>()
		);

		rooms = new List<Room>(this.GetComponentsInChildren<Room>());
		rooms.ForEach(room => room.AddEventListener(this.gameObject));

		docam = this.GetComponentInChildren<DoCam>();
		docam.enabled = false;
		hand = this.GetComponentInChildren<Hand>();
		hand.AddEventListener(this.gameObject);
		
		player = this.GetComponentInChildren<PlayerControl>();

		player.AddEventListener(this.gameObject);
		hand.AddEventListener(player.gameObject);

		LoadLastFinishedRoomType();
		
		if(lastEnteredRoomType != RoomType.NONE) {
			Room foundRoom = rooms.Find (room => room.roomType == lastEnteredRoomType);
			TutorialRoom tutorialRoom = foundRoom.GetComponent<TutorialRoom>();
			docam.enabled = true;
			DropPlayer(tutorialRoom.GetPlayerSpawnPoint());
		} else {
			DropPlayer();
		}
	}

	public virtual void OnGamePaused() {
		gamePaused = true;
	}

	public virtual void OnGameResumed() {
		gamePaused = false;
	}

	public override void OnArrivedAtStartPosition() {
		if(playerDead) {
			MaxLoader.LoadTutorial();
		} else if (tutorialDone) {
			PlayerPrefs.SetInt(GlobalSaveVars.TUTORIALFINISHED, 1);
			PlayerPrefs.Save();
			MaxLoader.LoadAfterTutorial();
		} 
	}

	public override void OnArrivedAtTargetPosition() {

		if(tutorialDone) {
			hand.Attach(player.gameObject);
			hand.Close();
			hand.SyncStartPosition(false);
			hand.MoveToStartPosition();
		} else if(!playerDead) {
			if(!gamePaused && gameTimer)
				gameTimer.OnResumeGame();

			hand.Open();
			hand.Detach();
			docam.enabled = true;
			hand.MoveToStartPosition();
		} else {
			hand.Attach(player.gameObject);
			hand.Close();
			hand.SyncStartPosition(false);
			hand.MoveToStartPosition();
		}

	}

	public override void OnPlayerDied() {
		playerDead = true;
		OnStopHills();
		PickupPlayer();
	}

	public void PickupPlayer() {
		hand.StopMoving();
		hand.ResetY();
		hand.SetMoveTarget(player.transform);
		hand.SyncPositionWithObject(player.gameObject, false);
		hand.MoveToTargetPosition();
		OnStopHills();
	}

	private void LoadLastFinishedRoomType() {
		string savedRoomType = PlayerPrefs.GetString(GlobalSaveVars.LASTENETEREDROOMNAME, "");
		Debug.Log (savedRoomType);

		if(savedRoomType != "") {
			RoomType roomType = (RoomType) Enum.Parse (typeof(RoomType), savedRoomType);
			lastEnteredRoomType = roomType;
		}
	}

	private void SaveLastFinishedRoomType() {
		PlayerPrefs.SetString (GlobalSaveVars.LASTENETEREDROOMNAME, lastEnteredRoomType.ToString());
		PlayerPrefs.Save();
	}

	public void OnTutorialDone() {
		tutorialDone = true;
		PickupPlayer();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(isInTutorialMode && !tutorialUsesTextBox) {
			if(Input.GetMouseButtonDown(0)) {

				isInTutorialMode = false;
				Invoke("ContinueGame", .2f);
			}
		}
	}

	private void ContinueGame() {
		output.enabled = false;
		PauseHelper.ResumeGame();
		if(animationForCurrentRoomType) {
			animationForCurrentRoomType.Hide();
			animationForCurrentRoomType.Stop();	
		}
	}

	public void OnStartTutorialAnimation(Animation2D animation2d) {
		tutorialUsesTextBox = false;
		if(!unlockedAnimations.Contains(animation2d.gameObject.name)) {
		
			OnStartTutorial();
			
			animationForCurrentRoomType = animation2d;
			animationForCurrentRoomType.SetOutputRenderer(output);
			animationForCurrentRoomType.Show();
			animationForCurrentRoomType.Play(true);

			unlockedAnimations.Add(animation2d.gameObject.name);
		}
	}

	public void OnStartTutorialWithTextBox() {
		tutorialUsesTextBox = true;
		output.sprite = null;
		OnStartTutorial();
	}

	private void OnStartTutorial() {
		PauseHelper.PauseGame();
		
		output.enabled = true;
		isInTutorialMode = true;
	}


	public void OnRoomEntered(Room room) {
		OnStartTutorialAnimation(
			tutorialAnimations.Find(room.roomType.ToString()).GetComponent<Animation2D>()
		);
		lastEnteredRoomType = room.roomType;
		SaveLastFinishedRoomType();
	}

	public void OnRoomExitted(RoomType roomType) {
		SaveTutorialsSeen();
	}

	private void LoadTutorialsSeen() {
		string saveString = PlayerPrefs.GetString(GlobalSaveVars.TUTORIALACTIONSEEN, "");
		Debug.Log(saveString);
		unlockedAnimations = new List<string>();

		if(saveString.Length > 0) {
			string[] splittedString = saveString.Split(seperator);
			foreach(string splitted in splittedString) {
				try {
					if(!unlockedAnimations.Contains(splitted)) {
						unlockedAnimations.Add(splitted);
					}
				} catch (ArgumentException) { 
					PlayerPrefs.SetString(GlobalSaveVars.TUTORIALACTIONSEEN, "");
					PlayerPrefs.Save();
					Debug.Log("corrupt save file!");
					
					return;
				}
			}
		}
	}

	private void SaveTutorialsSeen() {
		string saveString = "";
		
		for(int i = 0 ; i < unlockedAnimations.Count ; i++) {
			saveString += unlockedAnimations[i];

			if(i != unlockedAnimations.Count - 1) {
				saveString += seperator;
			}
		}

		PlayerPrefs.SetString(GlobalSaveVars.TUTORIALACTIONSEEN, saveString);	
		PlayerPrefs.Save();
	}
}
