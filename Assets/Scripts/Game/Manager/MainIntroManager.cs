using UnityEngine;
using System.Collections;

public class MainIntroManager : MonoBehaviour {

	private AudioSource backgroundMusic;

	public IntroExplanationManager introExplanationManager;
	public Camera mainCamera;
	public Transform cameraMovePosition;
	public float cameraSpeed = 10f;
	private YesButton yesButton;
	private Transform skipTutorialContainer;
	private Transform playContainerAltPosition;
	private Transform playContainer;

	// Use this for initialization
	void Start () {
		
		playContainer = this.transform.Find("PlayContainer");
		playContainerAltPosition = this.transform.Find("PlayContainerAltPosition");

		skipTutorialContainer = this.transform.Find("SkipTutorialContainer");
		backgroundMusic = GameObject.Find("IntroMusic").GetComponent<AudioSource>();
		DontDestroyOnLoad(backgroundMusic.gameObject);

		yesButton = this.transform.Find("PlayContainer/Yes").GetComponent<YesButton>();
		yesButton.AddEventListener(this.gameObject);

		int tutorialPassed = PlayerPrefs.GetInt(GlobalSaveVars.TUTORIALFINISHED, 0);
		if(tutorialPassed == 1) {
			skipTutorialContainer.active = true;
			skipTutorialContainer.GetComponentInChildren<SkipTutorialButton>().AddEventListener(this.gameObject);
			playContainer.position = playContainerAltPosition.position;
		}
	}
	
	public void OnSkipTutorial() {
		MaxLoader.LoadMainSceneFromIntro();
	}

	// Update is called once per frame
	void Update () {
	}

	public void OnYesButtonPressed() {
		MoveToExplanationPoint();
	}

	public void MoveToExplanationPoint() {
		introExplanationManager.active = true;
		MoveCameraTo(cameraMovePosition.position);
	}

	public void OnArrivedAtExplanationPoint() {
		introExplanationManager.Initialize();
	}

	private void MoveCameraTo(Vector3 target) {
		iTween.MoveTo(mainCamera.gameObject, iTween.Hash("position", target, "speed", cameraSpeed, "oncompletetarget", this.gameObject, "oncomplete", "OnArrivedAtExplanationPoint", "easetype", iTween.EaseType.linear));
	}
}
