using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void PauseGame() {
		List<CSBehaviour> pausableObjects = SceneUtils.FindObjectsOf<CSBehaviour>();
		foreach (CSBehaviour pausable in pausableObjects) {
			pausable.OnPauseGame();
		}
	}

	public static void ResumeGame() {
		List<CSBehaviour> pausableObjects = SceneUtils.FindObjectsOf<CSBehaviour>();
		foreach (CSBehaviour pausable in pausableObjects) {
			pausable.OnResumeGame();
		}
	}
}
