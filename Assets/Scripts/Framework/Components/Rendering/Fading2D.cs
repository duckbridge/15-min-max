using UnityEngine;
using System.Collections;

public class Fading2D : CSBehaviour {

	private bool isFading = false;
	public SpriteRenderer targetSprite;
	private float fadeTime;
	private Color targetColor;
	private Vector4 colorIncrement;
	private FadeType fadeType;

	public bool canBePaused = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isFading) {
			if(targetSprite.color != targetColor) {
				targetSprite.color += new Color(colorIncrement.x, colorIncrement.y, colorIncrement.z, colorIncrement.w);
			} else {
				OnFadingDone();
			}
		}
	}

	private void OnFadingDone() {
		if(isFading) {
			isFading = false;
			DispatchMessage("OnFadingDone", fadeType);
		}
	}

	public void SetTarget(SpriteRenderer targetSprite) {
		this.targetSprite = targetSprite;
	}

	public void FadeInto(Color newColor, float time = 60f, FadeType fadeType = FadeType.DEFAULT) {
		if(!this.targetSprite) {
			this.targetSprite = GetComponent<SpriteRenderer>();
		}
		
		targetColor = newColor;
		fadeTime = time;
		isFading = true;
		this.fadeType = fadeType;
		Vector4 colorDifference = (targetColor - targetSprite.color);
		colorIncrement = colorDifference / time;
	}

	public override void OnPauseGame() {
		if(canBePaused)
			this.enabled = false;
	}
	
	public override void OnResumeGame() {
		if(canBePaused)
			this.enabled = true;
	}
}
