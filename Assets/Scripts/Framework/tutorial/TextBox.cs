using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextBox: CSBehaviour {

	private List<TextContainer> textContainers = new List<TextContainer>();
	public float textTimeout;
	private string totalString;

	private int currentTextMeshIndex = 0;
	public bool isInitializedManually = false;
	// Use this for initialization
	void Awake () {
		if(!isInitializedManually) {
			Initialize();
		}
	}

	public void Initialize() {
		textContainers = new List<TextContainer>();
		TextMesh[] textMeshes = this.GetComponentsInChildren<TextMesh>();
		
		foreach(TextMesh textMesh in textMeshes) {
			TextContainer textContainer = new TextContainer(textMesh, true);
			textContainers.Add(textContainer);
		}
	}

	public void OnStart() {
		ShowNextWord();
	}

	private void ShowNextWord() {
		CancelInvoke("ShowNextWord");

		TextContainer currentTextContainer = textContainers[currentTextMeshIndex];

		if(currentTextContainer.CanDisplayNextWord()) {
			currentTextContainer.AppendNextWord();
			DispatchMessage("OnShowNextWord", null);
			Invoke("ShowNextWord", textTimeout);
		} else {

			if(currentTextMeshIndex < textContainers.Count - 1) {
				currentTextMeshIndex++;
				currentTextContainer = textContainers[currentTextMeshIndex];
				Invoke("ShowNextWord", textTimeout);
				return;
			}

			DispatchMessage("OnTextDone", this);
			Debug.Log("text done");
			return;
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
