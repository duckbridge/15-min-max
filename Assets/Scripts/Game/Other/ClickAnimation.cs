using UnityEngine;
using System.Collections;

public class ClickAnimation : Animation2D {

	private bool mouseDown = false;

	public void OnMouseUp() {
		this.mouseDown = false;
	}

	public void OnMouseDown() {
		this.mouseDown = true;
		Play(true);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnFrameEntered(int enteredFrame){
	}

	public override void OnFrameExited(int exitedFrame){

	}
}
