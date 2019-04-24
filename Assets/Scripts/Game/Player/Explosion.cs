using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float timeout = 5f;
	// Use this for initialization
	void Start () {
		Invoke("OnDestroy", timeout);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnDestroy() {
		Destroy(this.gameObject);
	}
}
