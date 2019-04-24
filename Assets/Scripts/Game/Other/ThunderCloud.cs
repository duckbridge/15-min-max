using UnityEngine;
using System.Collections;

public class ThunderCloud : Cloud {
	
	private float maxThunderTime = 3f;
	private float minThunderTime = 1f;
	private float flickerTimeout = .2f;

	private float thunderTime;
	private Thunder[] thunders;

	// Use this for initialization
	public override void Start () {
		base.Start();
		this.thunderTime = Random.Range(minThunderTime, maxThunderTime);
		thunders = this.GetComponentsInChildren<Thunder>();
		Invoke("DoThunder", thunderTime);
	}
	
	private void DoThunder() {
		Invoke("StopThunder", thunderTime);
		DoFlicker();
	}

	private void DoFlicker() {
		foreach(Thunder thunder in thunders) {
			thunder.renderer.enabled = true;
		}
		Invoke("StopFlicker", flickerTimeout);
	}

	private void StopFlicker() {
		foreach(Thunder thunder in thunders) {
			thunder.renderer.enabled = false;
		}
		Invoke("DoFlicker", flickerTimeout);
	}

	public void StopThunder() {
		CancelInvoke("StopThunder");
		CancelInvoke("DoFlicker");
		CancelInvoke("StopFlicker");

		Invoke("DoThunder", thunderTime);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
