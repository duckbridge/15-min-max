using UnityEngine;
using System.Collections;

public class IntroExplanationManager: MonoBehaviour {

	private Hand maxHand;

	private TextBoxManager textBoxManagerFirst;
	private MenuPlayerControl playerControl;

	public bool disableTimeout = true;
	private bool firstDialogueActive = true;

	// Use this for initialization
	void  Awake () {
		int timeRanOut = PlayerPrefs.GetInt(GlobalSaveVars.TIMERANOUTNAME, 0);
		
		if(timeRanOut == 1 && !disableTimeout) {
			MaxLoader.LoadAlternativeEndScene();
		}
		
		maxHand = this.transform.Find("MaxHand").GetComponent<Hand>();
		maxHand.AddEventListener(this.gameObject);
		textBoxManagerFirst = this.transform.Find("TextBoxManagerFirst").GetComponent<TextBoxManager>();

		textBoxManagerFirst.AddEventListener(this.gameObject);
		
		playerControl = this.GetComponentInChildren<MenuPlayerControl>();
		maxHand.AddEventListener(playerControl.gameObject);
	}

	public void Initialize() {
		maxHand.Show();
		textBoxManagerFirst.active = true;
	}

	public void OnTextDone() {
		textBoxManagerFirst.active = false;

		playerControl.Enable();
		maxHand.Open();
		maxHand.SyncPositionWithTarget(false);
		maxHand.MoveToTargetPosition();
	}

	public void OnArrivedAtTargetPosition() {
		maxHand.Attach(playerControl.gameObject, true);
		maxHand.Close();
		maxHand.SyncStartPosition(false);
		maxHand.MoveToStartPosition();
	}

	public void OnArrivedAtStartPosition() {
		MaxLoader.LoadTutorialFromIntro();
	}

	public void OnShowNextWord() {
		playerControl.OnStartTalking();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
