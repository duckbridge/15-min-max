using UnityEngine;
using System.Collections;

public class Flowers : MonoBehaviour {

	private SpriteRenderer flowersSprite;
	public float maxYOffset = 10;
	private float moveDownTime;

	public float percentageOfTimeSpentMovingDown = .2f;
	public float percentageOfTimeSpentMovingUp = .1f;

	// Use this for initialization
	void Awake () {
		flowersSprite = this.GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveInArc(Vector3 targetPosition, float moveTime) {

		iTween.MoveTo(this.gameObject,targetPosition, moveTime);
		iTween.MoveBy(flowersSprite.gameObject, 
		              iTween.Hash("name", "VerticalMoving", 
		                            "y", maxYOffset, 
		                            "time", moveTime * percentageOfTimeSpentMovingUp, 
		                            "oncompletetarget", this.gameObject, 
		                            "oncomplete", "MoveDown", 
		                            "easetype", iTween.EaseType.linear
		                            )
		              );
	}

	private void MoveDown() {
		flowersSprite.rigidbody2D.isKinematic = false;
	}
}
