using UnityEngine;
using System.Collections;

public class SlideEnemy : MonoBehaviour {

	private Animation2D sadAnimation;
	private Animation2D happyAnimation;
	private TriggerListener2D triggerListener;

	// Use this for initialization
	void Awake () {
		sadAnimation = this.transform.Find("Sad Animation").GetComponent<Animation2D>();
		happyAnimation = this.transform.Find("Happy Animation").GetComponent<Animation2D>();
		triggerListener = this.GetComponentInChildren<TriggerListener2D>();
		triggerListener.AddEventListener(this.gameObject);
		
	}
	
	public void OnListenerTrigger(Collider2D coll) {
		if(coll.gameObject.GetComponent<PlayerControl>()) {
			OnPlaySadAnimation();
		}
	}

	public void OnPlaySadAnimation() {
		happyAnimation.Stop();
		happyAnimation.Hide();
		sadAnimation.Show();
		sadAnimation.Play();
	}
}