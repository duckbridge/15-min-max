using UnityEngine;
using System.Collections;

public class BaseRoomTrigger : CSBehaviour {

	private bool canCollide = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEnter() {
		DispatchMessage("OnEntered", this);
	}

	protected void SpawnNextRoom() {
		SpawnManager spawnManager = SceneUtils.FindObjectOf<SpawnManager>();
		if(spawnManager)
			spawnManager.DoSpawn();
	}

	protected void OpenDoor() {
        Animation2D door = transform.Find("reddoor").GetComponent<Animation2D>();
        door.Animate();
	}

	public void OnTriggerEnter2D(Collider2D col) {
		if(canCollide) {
	    	if (col.tag == "Player") {
				canCollide = false;
	    		collider2D.enabled = false;
	        	OnEntered();
	        }
		}
    }

    public virtual void OnEntered() {
        SpawnNextRoom();

        OpenDoor();
        OnEnter();
    }
}
