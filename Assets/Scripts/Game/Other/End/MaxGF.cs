using UnityEngine;
using System.Collections;

public class MaxGF : MonoBehaviour {

	private Transform tearsContainer;
	public Flowers flowerPrefab;

	private Transform flowerThrowPoint;

	public Transform flowerMinThrowTarget;
	public Transform flowerMaxThrowTarget;

	public float minimumFlowerFlyTime = 1f;
	public float maximumFlowerFlyTime = 2f;

	private Vector2 flowerThrowDirection;
	private int flowersToThrow;
	public float flowerThrowTimeout = 1f;

	public Animation2D talkingAnimation;
	private PlayerTalking talking;
	public Fading2D blackFadeOverlay;

	private TextBox textBox;
	private bool isFading = false;
	private bool isDoneTalking = false;

	// Use this for initialization
	void Start () {
		tearsContainer = this.transform.Find ("TearsContainer");
		tearsContainer.active = false;

		flowersToThrow = PlayerPrefs.GetInt(GlobalSaveVars.HIGHSCOREVALUE, 0);
		flowerThrowPoint = this.transform.Find("FlowerThrowPoint");

		textBox = this.GetComponentInChildren<TextBox>();
		TextMesh textMeshWithReplacableContent = textBox.transform.Find("TextReplace").GetComponent<TextMesh>();
		string textBoxText = textMeshWithReplacableContent.text;
		Debug.Log (textBoxText);
		Debug.Log (textMeshWithReplacableContent);
		textMeshWithReplacableContent.text = textBoxText.Replace ("{R}", flowersToThrow+"");

		textBox.Initialize();
		textBox.AddEventListener(this.gameObject);
		textBox.active = false;

		talkingAnimation.Hide ();

		talking = this.GetComponentInChildren<PlayerTalking>();

		SceneUtils.FindObjectOf<ExitHandler>().AddEventListener(this.gameObject);
	}

	public void OnTextDone() {
		talkingAnimation.Stop ();
		talkingAnimation.Hide ();
		isDoneTalking = true;
	}

	public void OnShowNextWord() {
		talking.PlayRandomSound();
	}

	public void OnArrived() {
		StartCrying();
		ThrowFlower();

		textBox.active = true;
		talkingAnimation.Show ();
		talkingAnimation.Play ();
		this.transform.Find ("MaxGF Animation").GetComponent<Animation2D>().Pause();
		textBox.OnStart();
	}

	public void StartCrying() {
		tearsContainer.active = true;
	}

	private void DoThrow(Flowers flowers) {
		float moveTime = Random.Range (minimumFlowerFlyTime, maximumFlowerFlyTime);

		float flowerTargetX = Random.Range (flowerMinThrowTarget.position.x, flowerMaxThrowTarget.position.x);

		flowers.MoveInArc(new Vector3(
			flowerTargetX,
			flowerMinThrowTarget.position.y,
			flowers.transform.position.z
			), moveTime);
	}

	public void ThrowFlower() {
		CancelInvoke("ThrowFlower");

		Flowers flowers = (Flowers) GameObject.Instantiate(flowerPrefab, flowerThrowPoint.position, Quaternion.identity);
		DoThrow(flowers);
		--flowersToThrow;

		if(flowersToThrow > 0) {
			Invoke ("ThrowFlower", flowerThrowTimeout);
		} else {
			if(isDoneTalking)
				DoFade();
		}
	}

	public void OnQuitRequested() {
		DoFade();
	}

	public void DoFade() {
		if(!isFading) {
			isFading = true;
			blackFadeOverlay.AddEventListener(this.gameObject);
			blackFadeOverlay.FadeInto(Color.black, 120);
		}
	}

	public void OnFadingDone(FadeType fadeType) {
		MaxLoader.LoadThanksForPlaying();
	}
}
