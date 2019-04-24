using UnityEngine;
using System.Collections;

public class AfterIntroExplanationManager: MonoBehaviour {

	private Hand maxHand;
	private Hand chainHand;
	private GameObject bombChain;

	private TextBoxManager textBoxManagerAfterFirstDialogue;
	private MenuPlayerControl playerControl;

	public bool disableTimeout = true;
	private bool isUsingChainHand = true;
	private bool firstDialogueActive = true;

	private YesButton yesButton;
	private NoButton noButton;

	private Transform playButtons;
	public GameObject uiBombTimerContainer;
	private SoundObject bombAttachSound;
	public float bombAttachTimeout = .3f;

	void  Start () {
		playButtons = this.transform.Find("PlayButtons");
		yesButton = playButtons.GetComponentInChildren<YesButton>();
		yesButton.AddEventListener(this.gameObject);
		noButton = playButtons.GetComponentInChildren<NoButton>();
		noButton.AddEventListener(this.gameObject);

		playButtons.active = false;

		bombChain = this.transform.Find("BombChain").gameObject;
		maxHand = this.transform.Find("MaxHand").GetComponent<Hand>();
		bombAttachSound = this.transform.Find ("BombAttachSound").GetComponent<SoundObject>();
		maxHand.AddEventListener(this.gameObject);
		chainHand = this.transform.Find("ChainHand").GetComponent<Hand>();
		chainHand.AddEventListener(this.gameObject);

		textBoxManagerAfterFirstDialogue = this.transform.Find("TextBoxManagerAfterFirstDialogue").GetComponent<TextBoxManager>();
		textBoxManagerAfterFirstDialogue.AddEventListener(this.gameObject);

		textBoxManagerAfterFirstDialogue.active = false;

		playerControl = this.GetComponentInChildren<MenuPlayerControl>();
		maxHand.AddEventListener(playerControl.gameObject);

		AttachChainToMax();
	}

	private void AttachChainToMax() {
		chainHand.Close();
		chainHand.Attach(bombChain);
		chainHand.SyncPositionWithTarget(true);
		chainHand.MoveToTargetPosition();
		isUsingChainHand = true;
	}

	public void OnTextDone() {
		//pick max up
		playButtons.active = true;

		this.transform.Find ("mainCamera/ExitHandler/ExitButtonsContainer").active = false;
		SceneUtils.FindObjectOf<HardwareButtonsHandler>().active = false;

		textBoxManagerAfterFirstDialogue.active = false;	
		playerControl.ShowSadMouth();
	}

	public void OnArrivedAtTargetPosition() {
		if(!isUsingChainHand) {
			bombChain.active = false;
			maxHand.Attach(playerControl.gameObject, true);
			maxHand.Close();
			maxHand.SyncStartPosition(false);
			maxHand.MoveToStartPosition();
			playerControl.HideSadMouth();
		} else {
			bombAttachSound.GetSound().Play();
			Invoke("OnAttachBombToMaxsLeg", bombAttachTimeout);
		}	
	}

	private void OnAttachBombToMaxsLeg() {
		chainHand.Detach();
		chainHand.Open();
		chainHand.SyncStartPosition(true);
		chainHand.MoveToStartPosition();
		textBoxManagerAfterFirstDialogue.active = true;
		uiBombTimerContainer.animation.Play();
	}

	public void OnArrivedAtStartPosition() {
		if(!isUsingChainHand) {
			MaxLoader.LoadMainSceneFromIntro();
		}
	}
	
	public void OnYesButtonPressed() {
		playButtons.active = false;
		playerControl.Enable();
		maxHand.Open();
		maxHand.SyncPositionWithTarget(false);
		maxHand.MoveToTargetPosition();
		isUsingChainHand = false;
	}

	public void OnNoButtonPressed() {
		Application.Quit();
	}

	public void OnTextPartDone() {
		playerControl.ShowSadMouth();
	}

	public void OnShowNextTextBalloon() {
		playerControl.HideSadMouth();
	}

	public void OnShowNextWord() {
		playerControl.OnStartTalking();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
