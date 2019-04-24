using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxCarryComponent : MonoBehaviour {

	private List<Box> boxes = new List<Box>();
	public Box boxPrefab;
    
    private Transform boxPosition;
    private Transform secondBoxPosition;
    private float boxOffsetY;
    private Vector3 localBoxPosition;
    
	// Use this for initialization
	void Start () {
		boxPosition = this.transform.Find("Positions/BoxPosition");
        secondBoxPosition = this.transform.Find("Positions/SecondBoxPosition");
        boxOffsetY = Mathf.Abs(secondBoxPosition.localPosition.y - boxPosition.localPosition.y);
     	localBoxPosition = boxPosition.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnDropBox() {
		if(boxes.Count > 0) {
			boxes[0].OnDrop();
			boxes[0].transform.parent = null;
			boxes.RemoveAt(0);

			for(int i = 0 ; i < boxes.Count; i++) {
				boxes[i].transform.localPosition = 
				new Vector3(boxes[i].transform.localPosition.x, 
					boxes[i].transform.localPosition.y - boxOffsetY, 
					boxes[i].transform.localPosition.z
				); 
			}
		}

		if(boxes.Count == 0) {
			GetComponent<PlayerControl>().OnBoxDroppingDone();
    	}
	}

	public void AddBox(Box box) {
		box.transform.parent = this.transform;
		
		if(this.boxes.Count == 0) {
			box.transform.localPosition = boxPosition.localPosition;
		} else {
			box.transform.localPosition = new Vector3(boxPosition.localPosition.x, boxPosition.localPosition.y + boxOffsetY * (boxes.Count), boxPosition.localPosition.z);
		}

		Destroy(box.GetComponent<Rigidbody2D>());
		box.GetComponent<BoxCollider2D>().enabled = false;

		this.boxes.Add(box);
	}

	public void Enable() {
		if(boxes.Count > 0) {
			foreach(Box box in boxes) {
				box.GetRenderer().enabled = true;
			}
		}
	}

	public void Disable() {
		if(boxes.Count > 0) {
			foreach(Box box in boxes) {
				box.GetRenderer().enabled = false;
			}
		}
	}
}
