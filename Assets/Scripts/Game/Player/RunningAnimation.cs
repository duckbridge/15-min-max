using UnityEngine;
using System.Collections;

public class RunningAnimation : Animation2D {

	public ObjectsThatMoveWithHero objectsThatMoveWithHero;

	public override void OnFrameEntered(int enteredFrame){
		if(enteredFrame == 5) {
			objectsThatMoveWithHero.MoveDown();
		}
	}

	public override void OnFrameExited(int exitedFrame){
		if(exitedFrame == 5) {
			objectsThatMoveWithHero.Reset();
		}
	}
}
