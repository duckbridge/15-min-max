using UnityEngine;
using System.Collections;

public class FallTrigger : CSBehaviour {

    private FallEnemy fallEnemy;
    private Animation2D flickHand;

	void Start() {
		fallEnemy = this.transform.Find("FallEnemy").GetComponent<FallEnemy>();

        if(this.transform.Find("FlickHand")) {
            flickHand = this.transform.Find("FlickHand").GetComponent<Animation2D>();
            flickHand.AddEventListener(this.gameObject);
            flickHand.Hide();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
        if(player) {
            if(flickHand) { flickHand.Show(); flickHand.Play(true); }
			fallEnemy.RequestFall();
		}
    }

    public void OnAnimationDone(Animation2D animation2d) {
        flickHand.Hide();
    }
}
