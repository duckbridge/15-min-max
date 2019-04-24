using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager2D : MonoBehaviour {

	private List<Animation2D> animations;
	private Dictionary<string, Animation2D> animationsByName = new Dictionary<string, Animation2D>();
	private Animation2D currentAnimation;

	// Use this for initialization
	void Start () {
		animations = new List<Animation2D>(this.GetComponentsInChildren<Animation2D>());
		foreach(Animation2D animation in animations) {
			animationsByName.Add(animation.name, animation);
			if(!animation.playOnStartup)
				animation.Stop();
			else
				currentAnimation = animation;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void PlayAnimation(string animationName) {
		Animation2D foundAnimation;
		animationsByName.TryGetValue(animationName, out foundAnimation);

		if(foundAnimation) {
			currentAnimation.Stop();
			foundAnimation.Play();
			currentAnimation = foundAnimation;
		}
	}
}
