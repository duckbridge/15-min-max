using UnityEngine;
using System.Collections;

public class Cloud : ITweenMovableObject {
	
	private Fading2D fadeComponent;
	private SpriteRenderer spriteRenderer;
	private Color cloudOriginalColor;
	
	// Use this for initialization
	public virtual void Start () {
		Initialize();
	}

	protected override void Initialize() {
		if(!isInitialized) {
			this.spriteRenderer = this.GetComponent<SpriteRenderer>();
			this.goalPosition = goalTransform.localPosition;
			MoveToGoal();
			fadeComponent = this.GetComponent<Fading2D>();
			if(this.spriteRenderer)
				cloudOriginalColor = this.spriteRenderer.color;

			isInitialized = true;
		}
	}

	public void FadeIn() {
		Initialize();
		Color transparentColor = spriteRenderer.color;
		transparentColor.a = 0f;
		spriteRenderer.color = transparentColor;
		fadeComponent.FadeInto(cloudOriginalColor);
	}

	public void FadeOut() {
		Color transparentColor = spriteRenderer.color;
		transparentColor.a = 0f;
		fadeComponent.FadeInto(transparentColor);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
