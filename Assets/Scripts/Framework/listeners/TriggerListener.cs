using UnityEngine;
using System.Collections;

public class TriggerListener : CSBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider coll) {
		DispatchMessage("OnListenerTrigger", coll);
	}
}
