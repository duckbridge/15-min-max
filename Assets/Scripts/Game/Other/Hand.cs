using UnityEngine;
using System.Collections;

public class Hand : CSBehaviour {

	private Transform handContainer;
	public Vector3 offset; // y = -3.3
	
	public float handSpeed = 50f;
	public Transform moveTarget;
	private Vector3 startPos;
	private Vector3 handContainerLocalStartPos;

	private GameObject attachedObject;

	// Use this for initialization
	void Awake () {
		handContainer = this.transform.Find("HandContainer");
		startPos = this.transform.position;
		handContainerLocalStartPos = handContainer.localPosition;
	}
	
	// Update is called once per frame
	void Update () {}

	public void Open() {
		this.handContainer.Find("HandAnimation").GetComponent<Animation2D>().SetFrame(1);
	}

	public void Close() {
		this.handContainer.Find("HandAnimation").GetComponent<Animation2D>().SetFrame(0);
	}

	public void Attach(GameObject attachedObject, bool doDispatch = true) {
		this.attachedObject = attachedObject;
		this.attachedObject.transform.parent = this.handContainer;
		this.attachedObject.transform.localPosition = new Vector3(offset.x, offset.y, attachedObject.transform.localPosition.z);
		
		if(doDispatch)
			DispatchMessage("OnAttachedToHand", null);
	}

	public GameObject Detach(bool doDispatch = true) {
		this.attachedObject.transform.parent = null;

		if(doDispatch)
			DispatchMessage("OnDetachedFromHand", null);

		return attachedObject;
	}


	public void SetMoveTarget(Transform newTarget) {
		this.moveTarget = newTarget;
	}

	public void StopMoving() {
		iTween.StopByName(this.handContainer.gameObject, "HandMoving");
		handContainer.transform.localPosition = handContainerLocalStartPos;
	}

	public void ResetY() {
		this.transform.position = new Vector3(this.transform.position.x, startPos.y, this.transform.position.z);
	}

	public void SyncPositionWithTarget(bool syncY = true) {
		if(syncY) {
			this.transform.position = new Vector3(this.transform.position.x, moveTarget.transform.position.y, this.transform.position.z);
		} else {
			this.transform.position = new Vector3(moveTarget.transform.position.x, this.transform.position.y, this.transform.position.z);
		}
	}

	public void SyncPositionWithObject(GameObject gameobj, bool syncY = true) {
		if(syncY) {
			this.transform.position = new Vector3(this.transform.position.x, gameobj.transform.position.y, this.transform.position.z);
		} else {
			this.transform.position = new Vector3(gameobj.transform.position.x, this.transform.position.y, this.transform.position.z);
		}
	}

	public void SyncStartPosition(bool syncY = true) {
		if(syncY) {
			this.startPos = new Vector3(startPos.x, this.transform.position.y, startPos.z);
		} else {
			this.startPos = new Vector3(this.transform.position.x, startPos.y, startPos.z);
		}
	}

	public void MoveToStartPosition() {
		Move(startPos, handSpeed, "OnArrivedAtStartPosition");
	}

	public void MoveToTargetPosition() {
		Move(moveTarget.position, handSpeed, "OnArrivedAtTargetPosition");
	}

	private void OnArrivedAtTargetPosition() {
		DispatchMessage("OnArrivedAtTargetPosition", null);
	}

	private void OnArrivedAtStartPosition() {
		DispatchMessage("OnArrivedAtStartPosition", null);
	}

	protected void Move(Vector3 target, float speed, string onComplete) {
		iTween.MoveTo(handContainer.gameObject, iTween.Hash("name", "HandMoving", "position", target, "speed", speed, "oncompletetarget", this.gameObject, "oncomplete", onComplete, "easetype", iTween.EaseType.linear));
	}

	public void Show() {
		this.transform.Find("HandContainer").active = true;
	}

	public void Hide() {
		this.transform.Find("HandContainer").active = false;
	}
}
