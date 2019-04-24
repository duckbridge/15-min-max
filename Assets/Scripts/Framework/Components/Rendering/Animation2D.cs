using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Animation2D : CSBehaviour {
	
	public float FPS;
	private float secondsToWait;
	public bool Loop;
	public Sprite[] frames;
	public bool playOnStartup = false;

	public bool isPlayingReverse = false;
	private int currentFrame;

	protected bool stopped = false;
	protected bool paused = false;

	protected float animationSpeed = 1f;
	public SpriteRenderer outputRenderer;

	// Use this for initialization
	void Awake () {
		if(!outputRenderer)
			outputRenderer = this.GetComponent<SpriteRenderer>();
			
		currentFrame = 0;
		if(FPS > 0) 
			secondsToWait = 1/FPS;
		else 
			secondsToWait = 0f;

		if(playOnStartup)
			Play(true, isPlayingReverse);
	}
	
	public void SetOutputRenderer(SpriteRenderer outputRenderer) {
		this.outputRenderer = outputRenderer;
	}
	
	public void SetColor(Color newColor) {
		outputRenderer.color = newColor;
	}

	public void Animate()
	{	
		CancelInvoke("Animate");

		if(!isPlayingReverse) {
			if(currentFrame >= frames.Length) {
				if(!Loop) {
					stopped = true;
					DispatchMessage("OnAnimationDone", this);
				} else {
					currentFrame = 0;
					DispatchMessage("OnLoopingAnimationDone", this);
				}
			}
		} else {
			if(currentFrame <= -1) {
				if(!Loop) {
					stopped = true;
					DispatchMessage("OnReverseAnimationDone", this);
				} else {
					currentFrame = frames.Length - 1;
				}
			}
		}
		
		if(outputRenderer.enabled && !stopped) {
			if(!isPlayingReverse)
				OnFrameExited(currentFrame - 1);
			else
				OnFrameExited(currentFrame);

			outputRenderer.sprite = frames[currentFrame];

			if(!isPlayingReverse)
				OnFrameEntered(currentFrame);
			else
				OnFrameEntered(currentFrame - 1);
		}

		if(!isPlayingReverse)
			currentFrame++;
		else
			currentFrame--;
		
		if(!stopped && secondsToWait > 0)
			Invoke("Animate", secondsToWait/animationSpeed);
	}

	public void Play(bool reset = false, bool reverse = false) {
		this.isPlayingReverse = reverse;
		PlayWithReset(reset);
	}

	private void PlayWithReset(bool reset) {
		if(reset) currentFrame = 0;
		
		paused = false;
		stopped = false;
		outputRenderer.enabled = true;
		if(frames.Length > 1)
			Animate();
		else if(frames.Length > 0) {
			if(!isPlayingReverse)
				outputRenderer.sprite = frames[0];
			else 
				outputRenderer.sprite = frames[frames.Length - 1];
		}
	}

	public virtual void OnFrameEntered(int enteredFrame){}
	public virtual void OnFrameExited(int exitedFrame){}

	public void Pause() {
		stopped = true;
		paused = true;
		CancelInvoke("Animate");
	}

	public void Hide() {
		this.outputRenderer.enabled = false;
	}

	public void Show() {
		this.outputRenderer.enabled = true;
	}

	public void Stop() {
		Pause();
		if(!isPlayingReverse)
			currentFrame = 0;
		else
			currentFrame = frames.Length - 1;
	}

	public void SetSpeed(float newSpeed) {
		if(newSpeed > 0) {
			this.animationSpeed = newSpeed;
		}
	}

	public float GetSpeed() {
		return this.animationSpeed;
	}

	public void SetFrame(int newFrame) {
		if(newFrame > -1 && newFrame < frames.Length) {
			currentFrame = newFrame;
			outputRenderer.sprite = frames[currentFrame];
		}
	}

	public bool IsPlaying() {
		return !this.stopped;
	}

	public override void OnPauseGame() {}

	public override void OnResumeGame() {}
}