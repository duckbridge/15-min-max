using UnityEngine;
using System.Collections;

public class MenuPlayerControl : PlayerControl {
    private Transform sadMouth;
    private PlayerTalking playerTalking;

    public override void Awake() {
        this.sadMouth = this.transform.Find("SadMouth");
        this.sadMouth.active = false;
        this.lips = this.transform.Find("Lips").GetComponent<Animation2D>();
        this.lips.active = false;
        
        this.tearsContainer = this.transform.Find("Head/TearsContainer");
        this.tearsContainer.active = false;
        this.pickedUpEyesPosition = this.transform.Find("Head/PickupEyesPosition");
        this.animationController  = this.GetComponent<AnimationController>();

        playerTalking = this.GetComponentInChildren<PlayerTalking>();
    }

	public override void Update() {}
    public override void FixedUpdate() {}

    public override void Disable() {
        Disable(false);
    }

    public override void Disable(bool disableGravity = false) {}

    public override void Enable() {
        this.tearsContainer.active = true;
    }

    public void ShowSadMouth() {
        this.sadMouth.active = true;
    }

    public void HideSadMouth() {
        this.sadMouth.active = false;
    }

    public void OnStartTalking() {
        playerTalking.PlayRandomSound();
    }
}
