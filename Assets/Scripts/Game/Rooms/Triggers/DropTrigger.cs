using UnityEngine;
using System.Collections;

public class DropTrigger : CSBehaviour {


    void OnTriggerEnter2D(Collider2D col) {
    	Box box = col.gameObject.GetComponent<Box>();
      if(box) {
      		box.PlayDropSound();
        	DispatchMessage("OnBoxDropped", this);
      } 
    }
}
