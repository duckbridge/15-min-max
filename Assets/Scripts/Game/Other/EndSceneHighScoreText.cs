using UnityEngine;
using System.Collections;

public class EndSceneHighScoreText : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		int savedHighScore = PlayerPrefs.GetInt(GlobalSaveVars.HIGHSCOREVALUE, 0);
		this.GetComponent<TextMesh>().text = savedHighScore + "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
