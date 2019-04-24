using UnityEngine;
using System.Collections;

public class Cleaner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        print("ouch");

        if (other.tag == "Player")
        {
            print("touch!");
        }
        else
        {
            print("touch!");
            Destroy (other.gameObject);
        }
    }
}
