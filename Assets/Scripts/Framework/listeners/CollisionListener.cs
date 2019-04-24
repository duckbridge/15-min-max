using UnityEngine;
using System.Collections;

public class CollisionListener : CSBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCollisionEnter(Collision coll) {
		DispatchMessage("OnListenerCollision", coll);
	}
}
