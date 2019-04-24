using UnityEngine;
using System.Collections;

public class DebugReplayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClicked(RaycastHit hitSummary) {
		PlayerPrefs.DeleteAll();
		Application.LoadLevel("TitleIntroScene");
	}
}
