using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Loader : CSBehaviour {

	private enum LoaderState { PreLoading, Loading, Done, None}
	private LoaderState currentLoaderState = LoaderState.None;
	private Scene loadingScene;
	private Scene currentScene;
	
	private static bool isRemovingDuplicates = false;

	// Use this for initialization
	void Start () {
		if(transform.parent) {
			Debug.Log("[LOADER] WARNING! Loader should NOT have a parent object!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentLoaderState) {
			case LoaderState.PreLoading:
				DontDestroyOnLoad(transform.gameObject);
				Application.LoadLevel(loadingScene.ToString());
				currentLoaderState = LoaderState.Loading;
			break;
			case LoaderState.Loading:
				if(!Application.isLoadingLevel) {
					currentLoaderState = LoaderState.Done;
				}
			break;
			case LoaderState.Done:
				currentScene = loadingScene;
				DispatchMessage("OnLoadingDone", null);
				currentLoaderState = LoaderState.None;
				Destroy (this.gameObject);
			break;
		}
	}

	private void RemoveDuplicateLoaderObjects() {
		if(!Loader.isRemovingDuplicates) {
			Loader.isRemovingDuplicates = true;

			List<Loader> existingLoaders = SceneUtils.FindObjectsOf<Loader>();
			Debug.Log("loader count " + existingLoaders.Count);
			if(existingLoaders.Count > 1) {
				for(int i = 1 ; i < existingLoaders.Count; i++) {
					Destroy(existingLoaders[i].gameObject);
				}
			}
			Loader.isRemovingDuplicates = false;
		}
	}

	public void LoadScene(Scene scene) {
		loadingScene = scene;
		currentLoaderState = LoaderState.PreLoading;
	}

	public Scene GetCurrentScene() {
		return currentScene;
	}
}
