using UnityEngine;
using System.Collections;

public class Room : CSBehaviour {

	public int minimumScoreRequired = 0;
	public int maximumScoreRequired = 99;

	public RoomType roomType;
	protected BaseRoomTrigger baseRoomTrigger;
	protected BackdoorTrigger backdoorTrigger;
	protected PlayerControl player;
	public RoomLevel roomLevel;

	// Use this for initialization
	void Start () {
		baseRoomTrigger = this.GetComponentInChildren<BaseRoomTrigger>();
		baseRoomTrigger.AddEventListener(this.gameObject);
		
		backdoorTrigger = this.GetComponentInChildren<BackdoorTrigger>();
		if(backdoorTrigger)
			backdoorTrigger.AddEventListener(this.gameObject);
		
		player = SceneUtils.FindObjectOf<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {}

	public void OnEntered(BaseRoomTrigger baseRoomTrigger) {
		RoomTypeManager roomtypeManager = SceneUtils.FindObjectOf<RoomTypeManager>();
		if(roomtypeManager)
			roomtypeManager.OnRoomFinished(this.roomType);
		baseRoomTrigger.RemoveEventListener(this.gameObject);
		player.OnEnterRoom(this.roomType);
		
		DoExtraOnEntered();

		DispatchMessage("OnRoomEntered", this);
	}

	protected virtual void DoExtraOnEntered() {}

	public void OnPlayerExittedDoor(BackdoorTrigger backdoorTrigger) {
		player.OnExitRoom(this.roomType);

		DoExtraOnExit();
	}

	protected virtual void DoExtraOnExit() {
		IncreaseScore();
	}

	protected void IncreaseScore() {
        ScoreUI scoreUI = SceneUtils.FindObjectOf<ScoreUI>();
        if(scoreUI)
        	scoreUI.IncreaseNormalScore(1);
    }
}
