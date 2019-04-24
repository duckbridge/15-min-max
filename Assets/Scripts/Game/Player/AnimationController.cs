using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	private Animation2D currentAnimation;
	private string currentAnimationName;

	private Animation2D currentAdditiveAnimation;
	public string startAnimationName = "Running Animation";
	// Use this for initialization
	void Start () {
		if(startAnimationName.Length > 0)
        	PlayAnimation(startAnimationName);
	}
	

	private void SwitchAnimation(Animation2D newAnimation, bool reset = true, bool play = true, bool reverse  = false) {
		
		if(currentAdditiveAnimation) {
			currentAdditiveAnimation.Stop();
			currentAdditiveAnimation.Hide();
		}
		if(currentAnimation) {
			currentAnimation.Stop();
			currentAnimation.Hide();
		}

		currentAnimation = newAnimation;
		currentAnimation.Show();

		if(play)
			currentAnimation.Play(reset, reverse);
	}

	public void ShowAnimationAdditive(string name) {
		Animation2D found = GetAnimationByName(name);
		currentAdditiveAnimation = found;
		currentAdditiveAnimation.Show();
	}

	public Animation2D PlayAnimationAdditive(string name, bool reset = true, bool reverse = false) {
		Animation2D found = GetAnimationByName(name);
		currentAdditiveAnimation = found;
		currentAdditiveAnimation.Play(reset, reverse);

		return currentAdditiveAnimation;
	}

	public void StopAndHideAdditiveAnimation() {
		if(currentAdditiveAnimation) {
			currentAdditiveAnimation.Stop();
			currentAdditiveAnimation.Hide();
		}
	}

	public void StopAnimation() {
		if(currentAnimation)
			currentAnimation.Stop();
	}

	public void PauseAnimation() {
		if(currentAnimation)
			currentAnimation.Pause();
	}

	public void ResumeAnimation() {
		if(currentAnimation)
			currentAnimation.Play();
	}

	public Animation2D PlayAnimation(string name, bool reset = true, bool reverse = false) {
		Animation2D found = GetAnimationByName(name);
		
		if(found != currentAnimation) {
			SwitchAnimation(found, reset, true, reverse);
		} else {
			if(!currentAnimation.IsPlaying()) {
				currentAnimation.Play(reset, reverse);
			}
		}

		return found;
	}

	public void SetAnimationSpeed(string name, float newSpeed) {
		Animation2D found = GetAnimationByName(name);
		found.SetSpeed(newSpeed);
	}

	public float GetAnimationSpeed(string name) {
		Animation2D found = GetAnimationByName(name);
		return found.GetSpeed();
	}

	public Animation2D GetCurrent() {
		return this.currentAnimation;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public Animation2D GetAnimationByName(string name) {
		return this.transform.Find("Animations/"+name).GetComponent<Animation2D>();
	}
}
