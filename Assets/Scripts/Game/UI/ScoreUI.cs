using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour {

	private Score highScore;
	private Score normalScore;
	
	private AudioSource scoreSound;
	private AudioSource highScoreSound;

	// Use this for initialization
	void Awake () {
		highScore = this.transform.Find("HighScore").GetComponentInChildren<Score>();
		normalScore = this.transform.Find("Score").GetComponentInChildren<Score>();
		
		scoreSound = this.transform.Find("ScoreSounds/NormalScoreSound").GetComponent<AudioSource>();
		highScoreSound = this.transform.Find("ScoreSounds/HighScoreSound").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseNormalScore(int amount) {
		normalScore.IncreaseScore(amount);
		if(GetNormalScore() > GetHighScore()) {
			SetHighScore(GetNormalScore());
			highScoreSound.Play();
		} else {
			scoreSound.Play();
		}
	}

	public void SetHighScore(int newHighScore) {
		highScore.SetScore(newHighScore);
	}

	public int GetHighScore() {
		return highScore.GetScore();
	}

	public int GetNormalScore() {
		return normalScore.GetScore();
	}
}
