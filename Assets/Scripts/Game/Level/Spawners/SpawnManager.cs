using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public Spawner currentSpawner;
	public bool useOverrideSpawner = false;
	private PlayerControl player;

	void Awake () {
		if(useOverrideSpawner) {
			currentSpawner = this.GetComponentInChildren<OverrideSpawner>();
		}
		player = SceneUtils.FindObjectOf<PlayerControl>();
		player.AddEventListener(this.gameObject);
	}
	
	private void BeforeSpawn() {
		currentSpawner.BeforeSpawn();
	}

	private void AfterSpawn(){
		currentSpawner.AfterSpawn();
	}

	
	public void DoSpawn() {
		BeforeSpawn();
		currentSpawner.Spawn();
		AfterSpawn();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void OnRoomExitted(RoomType roomtype) {
		currentSpawner.OnRoomExitted(roomtype);
	}
}
