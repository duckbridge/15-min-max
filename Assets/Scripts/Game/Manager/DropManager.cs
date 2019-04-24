using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropManager : MonoBehaviour {
 
 	public GameObject DeathWall;
    public Animation2D bookcaseAnimation;
    public Animation2D doorAnimation;

	private List<DropTrigger> dropTriggers;
	private List<DropTrigger> finishedDropTriggers;

	void Awake () {
	}
	
	public void OnActivate() {
		dropTriggers = new List<DropTrigger>(this.GetComponentsInChildren<DropTrigger>());
		dropTriggers.ForEach(dropTrigger => dropTrigger.AddEventListener(this.gameObject));
		finishedDropTriggers = new List<DropTrigger>(dropTriggers);
	}

	public void OnBoxDropped(DropTrigger dropTrigger) {
		if(dropTriggers.Contains(dropTrigger)) {
			finishedDropTriggers.Remove(dropTrigger);
		}

		if(finishedDropTriggers.Count == 0) {
			DeathWall.GetComponent<BoxCollider2D>().enabled = false;
			doorAnimation.Play(true);
			bookcaseAnimation.Play(true);
  		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
