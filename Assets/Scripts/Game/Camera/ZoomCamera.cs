using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour {

	private Vector3 positionBeforeZooming;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ZoomInOn(GameObject targetObject) {
		this.positionBeforeZooming = this.transform.position;
		this.transform.position =  new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, this.transform.position.z);
		this.camera.orthographicSize = 10;
		Invoke ("ZoomOut", 2f);
	}

	public void ZoomOut() {
		this.transform.position = positionBeforeZooming;
		this.camera.orthographicSize = 50;
	}
}
