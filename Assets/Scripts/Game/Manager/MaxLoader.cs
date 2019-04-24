using UnityEngine;
using System.Collections;

public class MaxLoader : MonoBehaviour {
	private Loader loader;
	
	private SoundObject backgroundMusic;
	private static bool isLoadedFromIntroScene = false;
	private static bool isLoadingEndScene = false;
	public static Scene sceneToLoad;
	
	private SpriteRenderer happyMax;
	private SpriteRenderer sadMax;

	// Use this for initialization
	void Awake () {
		loader = this.GetComponent<Loader>();
		loader.AddEventListener(this.gameObject);
		loader.LoadScene(sceneToLoad);
		
		sadMax = this.transform.Find("Sprites/headlogoSad").GetComponent<SpriteRenderer>();
		happyMax = this.transform.Find("Sprites/headlogoHappy").GetComponent<SpriteRenderer>();

		if(isLoadedFromIntroScene) {
			backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<SoundObject>();
			DontDestroyOnLoad(backgroundMusic);
		}

		if(sceneToLoad == Scene.EndScene) {
			backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<SoundObject>();
			if(backgroundMusic)
				backgroundMusic.Mute();
		}

		if(sceneToLoad == Scene.FinalMainScene 
			|| sceneToLoad == Scene.EndScene || sceneToLoad == Scene.AlternativeEndScene) {
			
			sadMax.enabled = true;
			happyMax.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnLoadingDone() {
		if(isLoadedFromIntroScene) {
			Destroy(GameObject.Find("IntroMusic"));
			backgroundMusic.GetSound().Play();
			isLoadedFromIntroScene = false;
		} 

		if(isLoadingEndScene) {
			backgroundMusic = SceneUtils.FindObjectOf<SoundObject>(); //kind of cheat
			Destroy(backgroundMusic.gameObject);
			isLoadingEndScene = false;
		}
	}

	public static void LoadMainSceneFromIntro() {
		isLoadedFromIntroScene = true;
		sceneToLoad = Scene.FinalMainScene;
		
		Application.LoadLevel("Empty");
		Application.LoadLevelAdditive("Loading");
	}

	public static void LoadAlternativeEndScene() {
		sceneToLoad = Scene.AlternativeEndScene;
		
		Application.LoadLevel("Empty");
		Application.LoadLevelAdditive("Loading");
	}

	public static void LoadEndScene() {
		sceneToLoad = Scene.EndScene;

		Application.LoadLevel("Empty");
		Application.LoadLevelAdditive("Loading");
	}

	public static void LoadTutorialFromIntro() {
		
		PlayerPrefs.SetString(GlobalSaveVars.TUTORIALACTIONSEEN, "");	
		PlayerPrefs.Save();
		PlayerPrefs.SetString(GlobalSaveVars.LASTENETEREDROOMNAME, "");
		PlayerPrefs.Save ();

		LoadTutorial();
	}

	public static void LoadThanksForPlaying() {
		sceneToLoad = Scene.ThanksForPlaying;

		Application.LoadLevel("Empty");
		Application.LoadLevelAdditive("Loading");
	}

	public static void LoadTutorial() {
		sceneToLoad = Scene.Tutorial;
		
		Application.LoadLevel("Empty");
		Application.LoadLevelAdditive("Loading");
	}

	public static void LoadAfterTutorial() {
		sceneToLoad = Scene.AfterTutorial;
		
		Application.LoadLevel("Empty");
		Application.LoadLevelAdditive("Loading");
	}

	public static void LoadIntroScene() {
		sceneToLoad = Scene.TitleIntroScene;
		
		Application.LoadLevel("Empty");
		Application.LoadLevelAdditive("Loading");
	}
}
