using UnityEngine;
using System.Collections;

public class OnTutorialDoneTrigger : CSBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col) {
    	PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
        if (player) {
           	player.Disable();
			player.GetComponent<AnimationController>().PauseAnimation();
			DispatchMessage("OnTutorialDone", null);
        }
    }
}
