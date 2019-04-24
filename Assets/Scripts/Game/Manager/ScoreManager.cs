using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	void Start () {
		LoadScore();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int LoadScore() {
		int highScore = PlayerPrefs.GetInt(GlobalSaveVars.HIGHSCOREVALUE, 0);
		SceneUtils.FindObjectOf<ScoreUI>().SetHighScore(highScore);
		return highScore;
	}

	public void SaveScore() {
		int score = SceneUtils.FindObjectOf<ScoreUI>().GetNormalScore();
		
		if(IsHighScore(score)) {
			PlayerPrefs.SetInt(GlobalSaveVars.HIGHSCOREVALUE, score);
			PlayerPrefs.Save();
		}
	}

	private bool IsHighScore(int score) {
		int highScore = PlayerPrefs.GetInt(GlobalSaveVars.HIGHSCOREVALUE, 0);
		return (score > highScore);
	}
}
