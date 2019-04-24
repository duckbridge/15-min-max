using UnityEngine;
using System.Collections;

public class ClickAnimationHandler : MonoBehaviour {

	public Camera usedCamera;

	private ClickAnimation mouseClickAnimation;
	private Transform animations;

	private bool isMouseDown = false;
	private bool isShowingSecondPart = false;
	// Use this for initialization

	void Awake () {
		animations = this.transform.Find("Animations");
		mouseClickAnimation = this.animations.Find("ClickAnimation").GetComponent<ClickAnimation>();
		mouseClickAnimation.AddEventListener(this.gameObject);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!isMouseDown) {
			if(Input.GetMouseButtonDown(0)) {
				isMouseDown = true;
				mouseClickAnimation.OnMouseDown();
			}
		}

		if(isMouseDown) {
			Vector3 worldPosition = usedCamera.ScreenToWorldPoint(Input.mousePosition);
			animations.position = new Vector3(worldPosition.x, worldPosition.y, animations.position.z);
		}

		if(Input.GetMouseButtonUp(0)) {
			isMouseDown = false;
			Vector3 worldPosition = usedCamera.ScreenToWorldPoint(Input.mousePosition);
			animations.position = new Vector3(worldPosition.x, worldPosition.y, animations.position.z);
		
			mouseClickAnimation.OnMouseUp();
		}
	}

	public void OnAnimationDone(Animation2D animation2d) {
		mouseClickAnimation.Hide();
	}
}
