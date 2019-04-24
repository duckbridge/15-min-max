using UnityEngine;
using System.Collections;

public class ITweenMovableObject : MonoBehaviour {
	
	public Transform goalTransform;
	public Transform startTransform;

	protected Vector3 goalPosition;
	public float speed = 2f;
	protected bool isInitialized = false;

	// Use this for initialization
	public virtual void Start () {
		Initialize();
	}

	protected virtual void Initialize() {
		if(!isInitialized) {
			this.goalPosition = goalTransform.localPosition;
			isInitialized = true;
		}
	}
	
	protected void ResetAndMove() {
		this.goalPosition = goalTransform.localPosition;
		this.transform.localPosition = new Vector3(startTransform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);

		MoveToGoal();
	}

	public void Stop() {
		iTween.StopByName(this.gameObject, "moveObject");
	}

	public void MoveToGoal() {
		iTween.MoveTo(gameObject, iTween.Hash("name", "moveObject","x",goalPosition.x,"isLocal",true, "speed",speed,"oncomplete", "ResetAndMove", "easetype", iTween.EaseType.linear));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
