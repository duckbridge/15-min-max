using UnityEngine;
using System.Collections;

public class ObjectsThatMoveWithHero : MonoBehaviour {

	private Vector3 moveTargetPosition;
	private Vector3 originalPosition;

	// Use this for initialization
	void Awake () {
		this.originalPosition = this.transform.localPosition;
		this.moveTargetPosition = this.transform.Find("MoveTarget").localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveDown() {
		this.transform.localPosition = new Vector3(this.transform.localPosition.x, moveTargetPosition.y, this.transform.localPosition.z);
	}

	public void Reset() {
		this.transform.localPosition = new Vector3(this.transform.localPosition.x, originalPosition.y, this.transform.localPosition.z);
	}
}
