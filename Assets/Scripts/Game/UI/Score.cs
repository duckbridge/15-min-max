using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	private int score = 0;
	private TextMesh scoreOutput;

	public void Awake() {
		scoreOutput = this.transform.Find("Output").GetComponent<TextMesh>();
	}

    public void IncreaseScore(int amount) { 
        score += amount;
        scoreOutput.text = score+"";
    }

    public int GetScore() {
    	return score;
    }

    public void SetScore(int newScore) {
    	this.score = newScore;
    	scoreOutput.text = score+"";
    }
}
