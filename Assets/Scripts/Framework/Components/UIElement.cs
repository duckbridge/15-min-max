using UnityEngine;
using System.Collections;

public class UIElement : CSBehaviour {

	public Transform showPosition;
	public Transform hidePosition;

	public bool isShown = true;
	public float hideShowTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleVisibility() {
		if(isShown) {
			isShown = false;
			Hide();
		} else {
			isShown = true;
			Show();
		}
	}

	public virtual void Hide() {
		iTween.MoveTo(this.gameObject, this.hidePosition.position, hideShowTime);
		this.isEnabled = false;
	}

	public virtual void Show() {
		iTween.MoveTo(this.gameObject, this.showPosition.position, hideShowTime);
		this.isEnabled = true;
	}

}
