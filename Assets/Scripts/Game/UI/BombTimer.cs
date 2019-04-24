using UnityEngine;
using System.Collections;

public class BombTimer : MonoBehaviour {

	private float maxScale = 100;
	private float currentScale;
	private float scaleDecreaseAmount;
	
	private Transform lontContainer;
	private Transform flame;
	private Transform endOfFuse;
	private Transform timerBombBig;

	public float startTimeInms;
	private GameTimer gameTimer;

	private bool isFirstTick = true;

	// Use this for initialization
	void Awake () {
		lontContainer = this.transform.Find("LontContainer");
		flame = this.transform.Find("Flame");
		endOfFuse = lontContainer.Find("EndofFuse");
		timerBombBig = this.transform.Find("TimerBombBig");

		lontContainer.active = false;
		flame.active = false;
		timerBombBig.active = false;
		
		
		gameTimer = SceneUtils.FindObjectOf<GameTimer>();
		gameTimer.AddEventListener(this.gameObject);
		
	}
	
	private void StartBombTimer() {
		startTimeInms = gameTimer.GetOriginalStartTimeInMs();

		lontContainer.active = true;
		flame.active = true;
		timerBombBig.active = true;

		scaleDecreaseAmount = (lontContainer.localScale.x/gameTimer.GetOriginalStartTimeInS());
		currentScale = lontContainer.localScale.x;
	}

	// Update is called once per frame
	void Update () {}

	public void OnSecondsTick(int secondsLeft) {
		if(isFirstTick) {
			StartBombTimer();
			isFirstTick = false;
		}

		if(lontContainer)
			lontContainer.localScale = new Vector3(
				scaleDecreaseAmount * secondsLeft, 
				lontContainer.localScale.y, 
				lontContainer.localScale.z);
		
		flame.transform.position = endOfFuse.position;
	}

	void FixedUpdate() {

	}
}
