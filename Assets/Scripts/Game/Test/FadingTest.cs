using UnityEngine;
using System.Collections;

public class FadingTest : MonoBehaviour {
	private Fading2D fading2D;
	public Color targetColor;

	// Use this for initialization
	void Start () {
		fading2D = this.GetComponent<Fading2D>();
		fading2D.FadeInto(targetColor, 50f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
