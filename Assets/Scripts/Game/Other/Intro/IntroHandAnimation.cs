using UnityEngine;

public class IntroHandAnimation : MonoBehaviour {

	public Animation2D fakeAnimation;
	
	public void OnCloseHand() {
		fakeAnimation.SetFrame(0);
	}

	public void OnOpenHand() {
		fakeAnimation.SetFrame(1);
	}
}

