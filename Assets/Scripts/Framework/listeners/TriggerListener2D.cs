﻿using UnityEngine;
using System.Collections;

public class TriggerListener2D : CSBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D coll) {
		DispatchMessage("OnListenerTrigger", coll);
	}
}
