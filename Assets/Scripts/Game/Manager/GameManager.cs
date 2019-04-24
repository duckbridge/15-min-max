using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	protected PlayerControl player;
	protected GameTimer gameTimer;
	protected Hand hand;
	protected DoCam docam;
	private BombTimer bombTimer;
	protected bool gamePaused = false;
	protected PauseMenu pauseMenu;
	protected List<ITweenMovableObject> hills;
	public float backgroundMusicFadeOutSpeed = 0.0005f;

	public bool useSavedTime = true;
	protected bool playerDead = false;
	// Use this for initialization

	public virtual void Start () {

		hills = new List<ITweenMovableObject>(
			this.transform.Find("mainCamera/Hills").GetComponentsInChildren<ITweenMovableObject>()
		);

		pauseMenu = this.GetComponentInChildren<PauseMenu>();
		pauseMenu.AddEventListener(this.gameObject);
		pauseMenu.active = false;

		gameTimer = this.GetComponentInChildren<GameTimer>();
		gameTimer.OnPauseGame();

		docam = this.GetComponentInChildren<DoCam>();
		docam.enabled = false;
		
		hand = this.GetComponentInChildren<Hand>();
		hand.AddEventListener(this.gameObject);
		
		player = this.GetComponentInChildren<PlayerControl>();

		player.AddEventListener(this.gameObject);
		gameTimer.AddEventListener(this.gameObject);
		hand.AddEventListener(player.gameObject);

		float savedTime = PlayerPrefs.GetFloat(GlobalSaveVars.TIMELEFT, -1);
		bombTimer = this.GetComponentInChildren<BombTimer>();

		if(savedTime != -1 && useSavedTime) {
			gameTimer.SetStartTimeInMS(savedTime);
		}

		DropPlayer();
	}

	public void OnStopHills() {
		hills.ForEach(hill => hill.Stop());
	}

	public void OnStartHills() {
		hills.ForEach(hill => hill.MoveToGoal());
	}

	protected virtual void DropPlayer(Transform target = null) {
		hand.Attach(player.gameObject, true);
		hand.Close();
		hand.SyncPositionWithObject(player.gameObject, false);

		if(target) {
			hand.SetMoveTarget(target);
			hand.SyncPositionWithTarget(false);
		}

		hand.MoveToTargetPosition();
	}

	public virtual void OnArrivedAtTargetPosition() {
		if(!playerDead) {
			if(!gamePaused && gameTimer)
				gameTimer.OnResumeGame();

			hand.Open();
			hand.Detach();
			docam.enabled = true;
			hand.MoveToStartPosition();
			pauseMenu.active = true;
		} else {
			hand.Attach(player.gameObject);
			hand.Close();
			hand.SyncStartPosition(false);
			hand.MoveToStartPosition();
		}
	}

	public virtual void OnGamePaused() {
		gamePaused = true;
	}

	public virtual void OnGameResumed() {
		gamePaused = false;
	}

	public virtual void OnArrivedAtStartPosition() {
		if(playerDead) {
			Application.LoadLevel("Empty");
			Application.LoadLevelAdditive("Loading");
		}
	}

	public virtual void OnPlayerDied() {
		playerDead = true;
		pauseMenu.active = false;
		OnStopHills();
		DisableTimerAndSaveTime();
		hand.SetMoveTarget(player.transform);
		hand.SyncPositionWithObject(player.gameObject, false);
		hand.MoveToTargetPosition();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void OnGameTimerFinished(GameTimer gameTimer) {
		if(GameObject.Find ("BackgroundMusic")) {
			FadingAudio backgroundMusic = GameObject.Find ("BackgroundMusic").GetComponent<FadingAudio>();
			backgroundMusic.FadeOut(backgroundMusicFadeOutSpeed);
		}

		PlayerPrefs.SetInt(GlobalSaveVars.TIMERANOUTNAME, 1);
		PlayerPrefs.Save();
		player.OnExplode();
		OnStopHills();
		pauseMenu.active = false;
		bombTimer.active = false;
	}

	public void DisableTimerAndSaveTime() {
		gameTimer.OnPauseGame();
		float savedTime = gameTimer.GetCurrentTimeInMs();
		PlayerPrefs.SetFloat(GlobalSaveVars.TIMELEFT, savedTime);
		PlayerPrefs.Save();
	}
}
