using UnityEngine;
using System.Collections;

public class PlayerControl : CSBehaviour {

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
    private float originalMoveForce;
    public float moveForceIncrease = 10f;
	public float maxMoveForce = 5f;

	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public float animationSpeedIncreaseOnSprint = .2f;
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.

    //gun stuff
    public Bullet bullet;
    private Transform gun;

	//stop stuff
    public float stopDecrementer = 20f;
    
    //slide stuff
    public BoxCollider2D boxsmall;
    public BoxCollider2D boxbig;
    public CircleCollider2D feet;

    public float slideTimeout = 1f;
    public float swingTimeout = 1f;

    private Transform standEyesPosition;
    private Transform slideEyesPosition;
    protected Transform pickedUpEyesPosition;
    private Transform stopEyesPosition;
    protected Transform tearsContainer;

    public PlayerItem baseBallBat;
	private PlayerTalking talking;
    //JUMP STUFF
    private bool canJump = true;

    //Box
    private BoxCarryComponent boxCarryComponent;

    protected AnimationController animationController;

    private AudioSource jumpSound, onHitSound, boxDropSound, shootSound, swingSound, slideSound, stopSound, explosionSound;
    private RoomType currentRoomType = RoomType.NONE;
    
    private enum PlayerState{ RUNNING, HOLDINGGUN, JUMPING, INAIR, STOPPING, STOPPED, CARRYINGBOX, DYING, SLIDING, HOLDINGBAT, SWINGINGBASEBALLBAT }
    private PlayerState currentState = PlayerState.RUNNING;
    //ADD PREVIOUS PLAYER STATE TO SAVE STATE WHEN STOPPING AND THEN SETTING IT BACK TO CURRENT STATE!!!!!!
	private PlayerState previousState = PlayerState.RUNNING;

	protected Animation2D lips;
    private PlayerItemComponent playerItemComponent;
    private MaxGhost ghost;
    protected bool isPaused = false;

    public virtual void Awake() {
		talking = this.GetComponentInChildren<PlayerTalking>();

        originalMoveForce = this.moveForce;
        tearsContainer = this.transform.Find("Head/TearsContainer");
       
        this.standEyesPosition = this.transform.Find("Head/StandEyesPosition");
        this.slideEyesPosition = this.transform.Find("Head/SlideEyesPosition");
        this.pickedUpEyesPosition = this.transform.Find("Head/PickupEyesPosition");
        this.stopEyesPosition = this.transform.Find("Head/StopEyesPosition");

        this.animationController = this.GetComponent<AnimationController>();
        this.tearsContainer.position = standEyesPosition.position;

        groundCheck = transform.Find("groundCheck");
        gun = this.transform.Find("Items/Gun");

        this.jumpSound = this.transform.Find("Sounds/JumpSound").GetComponent<AudioSource>();
        this.onHitSound = this.transform.Find("Sounds/OnHitSound").GetComponent<AudioSource>();
        this.shootSound = this.transform.Find("Sounds/ShootSound").GetComponent<AudioSource>();
        this.swingSound = this.transform.Find("Sounds/SwingSound").GetComponent<AudioSource>();
        this.stopSound = this.transform.Find("Sounds/StopSound").GetComponent<AudioSource>();
        this.slideSound = this.transform.Find("Sounds/SlideSound").GetComponent<AudioSource>();
        this.explosionSound = this.transform.Find("Sounds/ExplosionSound").GetComponentInChildren<AudioSource>();

        this.lips = this.transform.Find("Lips").GetComponent<Animation2D>();
        this.lips.active = false;
        
        playerItemComponent = this.GetComponent<PlayerItemComponent>();
        this.AddEventListener(playerItemComponent.gameObject);
        boxCarryComponent = this.GetComponent<BoxCarryComponent>();

        ghost = this.GetComponentInChildren<MaxGhost>();
        ghost.AddEventListener(this.gameObject);
        ghost.active = false;

   }

	public virtual void Update() {
        
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));


        switch(currentRoomType) {
            case RoomType.JUMP:
				if(isPaused) {
					return;
				}

                if (Input.GetMouseButtonDown(0) && canJump) {
					SwitchState(PlayerState.JUMPING);
                    animationController.PlayAnimation("Jump Animation");
                    jumpSound.Play();
                }

                if(!grounded && currentState != PlayerState.DYING) {
                    animationController.PlayAnimation("Jump Animation");
					SwitchState(PlayerState.RUNNING);
                    canJump = false;
                }

                if(currentState == PlayerState.RUNNING && grounded) {
                    animationController.PlayAnimation("Running Animation");
                    canJump = true;
                }
            break;

            case RoomType.DROP:
				if(isPaused) {
					return;
				}

                if(currentState == PlayerState.CARRYINGBOX) {
                    animationController.PlayAnimation("Drop Animation");

                    if (Input.GetMouseButtonDown(0)) {
                        boxCarryComponent.OnDropBox();
                    }
                }
            break;

            case RoomType.SHOOT:
				if(isPaused) {
					return;
				}
                if (Input.GetMouseButtonDown(0) && currentState == PlayerState.HOLDINGGUN) {
                    Bullet bulletInstance = (Bullet) GameObject.Instantiate(bullet, new Vector3(gun.position.x, gun.position.y, gun.position.z + 0.05f), Quaternion.Euler(new Vector3(0, 0, 0)));
                    
                    animationController.PlayAnimationAdditive("Gun Animation", true);
                    shootSound.Play();
                }
            break;

            case RoomType.HIT:
				if(isPaused) {
					return;
				}
                if (Input.GetMouseButtonDown(0) && currentState == PlayerState.HOLDINGBAT) {
                    animationController.PlayAnimationAdditive("Baseball Animation", true);
                    animationController.GetAnimationByName("Baseball Animation").AddEventListener(this.gameObject);
                    baseBallBat.active = true;
                    swingSound.Play();
						
					SwitchState(PlayerState.SWINGINGBASEBALLBAT);
                }
            break;

            

            case RoomType.SLIDE:
				if(isPaused) {
					return;
				}
                if (Input.GetMouseButtonDown(0)) {
                    if (currentState == PlayerState.RUNNING) {
                        boxsmall.enabled = true;
                        boxbig.enabled = false;
                        feet.enabled = false;
						SwitchState(PlayerState.SLIDING);
                        slideSound.Play();
                        animationController.PlayAnimation("Slide Animation");
                        Invoke("StopSliding", slideTimeout);
                        this.tearsContainer.position = slideEyesPosition.position;
                    }  
                }
            break;

            case RoomType.SPRINT:
				if(isPaused) {
					return;
				}
                if (Input.GetMouseButtonDown(0)) {
                    if(moveForce < maxMoveForce) {
                        moveForce += moveForceIncrease;
						float currentAnimationSpeed = animationController.GetAnimationSpeed("Running Animation");
						animationController.SetAnimationSpeed("Running Animation", currentAnimationSpeed + animationSpeedIncreaseOnSprint);
                    }
                }
            break;

			case RoomType.STOP:
				switch(currentState) {
					case PlayerState.RUNNING:
						if(isPaused) {
							return;
						}
						if (Input.GetMouseButtonDown(0)) {
							animationController.PlayAnimation("Stop Animation");
							SwitchState(PlayerState.STOPPING);
							stopSound.Play();
							this.tearsContainer.position = this.stopEyesPosition.position;
						}
					break;
						
					case PlayerState.STOPPING:
						if(isPaused) {
							return;
						}
						
						if(moveForce > 0)
							moveForce -= stopDecrementer;
						
						if(moveForce <= 0) {
							stopSound.Stop();
							SwitchState(PlayerState.STOPPED);
							DispatchMessage("OnStopHills", null);
						}    
						
						if(Input.GetMouseButtonUp(0)) {
							stopSound.Stop();
							animationController.PlayAnimation("Running Animation");
							this.tearsContainer.position = this.standEyesPosition.position;
							moveForce = originalMoveForce;
							SwitchState(PlayerState.RUNNING);
						}
					break;
						
					case PlayerState.STOPPED:
						if(Input.GetMouseButtonUp(0)) {
							animationController.PlayAnimation("Running Animation");
							moveForce = originalMoveForce;
							SwitchState(PlayerState.RUNNING);
							DispatchMessage("OnStartHills", null);
						}
					break;
				}
			break;
		}
	}

    public void OnEnterRoom(RoomType roomtype) { 
        playerItemComponent.OnEnterRoom(roomtype);
        currentRoomType = roomtype;
       
        switch(roomtype){
            case RoomType.SHOOT:
                animationController.ShowAnimationAdditive("Gun Animation");
				SwitchState(PlayerState.HOLDINGGUN);
            break;

            case RoomType.HIT:
                animationController.ShowAnimationAdditive("Baseball Animation");
				SwitchState(PlayerState.HOLDINGBAT);
            break;

            case RoomType.DROP:
				SwitchState(PlayerState.CARRYINGBOX);
                animationController.PlayAnimation("Drop Animation");
            break;

            case RoomType.JUMP:
                canJump = true;
            break;
        }
    }

    public void OnExitRoom(RoomType roomtype) {
       DispatchMessage("OnRoomExitted", roomtype);
       switch(roomtype) {
            case RoomType.SHOOT:
                animationController.StopAndHideAdditiveAnimation();
				SwitchState(PlayerState.RUNNING);
            break;

            case RoomType.HIT:
                CancelInvoke("OnSwung");
                animationController.StopAndHideAdditiveAnimation();
				SwitchState(PlayerState.RUNNING);
            break;
            
            case RoomType.SPRINT:
                animationController.SetAnimationSpeed("Running Animation", 1);
                this.moveForce = originalMoveForce;
            break;

            case RoomType.JUMP:
				SwitchState(PlayerState.RUNNING);
                animationController.PlayAnimation("Running Animation");
                canJump = false;
            break;

            case RoomType.STOP: //to prevent bug
				SwitchState(PlayerState.RUNNING);
                animationController.PlayAnimation("Running Animation"); 
                this.moveForce = originalMoveForce;
            break;
        }
        
        currentRoomType = RoomType.NONE;
    }

    public void CarryBoxes(int amount) {
        for(int i = 0 ; i < amount ; i++) {
            Box newBox = (Box) GameObject.Instantiate(boxCarryComponent.boxPrefab, this.transform.position, Quaternion.identity);
            boxCarryComponent.AddBox(newBox);
        }
    }

    private void StopSliding() {
        CancelInvoke("StopSliding");
        if (currentState == PlayerState.SLIDING) {
            boxsmall.enabled = false;
            boxbig.enabled = true;
            feet.enabled = true;
            Animation2D slideAnimation = animationController.PlayAnimation("Slide Animation", true, true);
            slideAnimation.AddEventListener(this.gameObject);
            this.tearsContainer.position = standEyesPosition.position;
        }
    }

    public void OnAnimationDone(Animation2D animation2d) {
        animation2d.RemoveEventListener(this.gameObject);
        if(animation2d == animationController.GetAnimationByName("Baseball Animation")) {
			SwitchState(PlayerState.HOLDINGBAT);
            baseBallBat.active = false;
        } else {
            animation2d.active = false;
        }   
    }
    
    private void OnReverseAnimationDone(Animation2D animation2d) { //done with reverse slide animation
		SwitchState(PlayerState.RUNNING);
        animation2d.RemoveEventListener(this.gameObject);
        animationController.PlayAnimation("Running Animation");
    }

    public virtual void FixedUpdate () {

        if(currentState != PlayerState.DYING && !isPaused) {

            rigidbody2D.velocity = new Vector2(moveForce, rigidbody2D.velocity.y);
            
            if(currentState == PlayerState.JUMPING && grounded) {
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));

				SwitchState(PlayerState.INAIR);
                animationController.PlayAnimation("Running Animation");
            }
        }

	}
	    
    public void OnExplode() {
        Disable();
        explosionSound.Play();
        animationController.PlayAnimation("Explosion Animation");
        transform.Find("Items/Umbrella").active = false;
        lips.active = false;
        this.rigidbody2D.isKinematic = true;
        this.boxbig.enabled = false;
        this.boxsmall.enabled = false;

        ghost.active = true;
        ghost.FlyUp();
    }

    public void OnGhostFlyingDone() {
        LoadDeathScene();
    }

    public void OnAttachedToHand() {
        Disable(true);
        SetTearsCorrectlyForPickup();
        TurnOnTears();
        animationController.PlayAnimation("PickedUp Animation");
    }

    public void OnDetachedFromHand() {
        animationController.PlayAnimation("Running Animation");
        Enable();
        this.tearsContainer.position = this.standEyesPosition.position;
    }

    public void OnDie() {
        if(currentState != PlayerState.DYING) {
            Disable();
            this.onHitSound.Play();
            animationController.PlayAnimation("Death Animation");
            SetTearsCorrectlyForPickup();
            ScoreManager scoreManager = SceneUtils.FindObjectOf<ScoreManager>();
            if(scoreManager)
                scoreManager.SaveScore();  
            DispatchMessage("OnPlayerDied", null);
        }
    }

    public void SetTearsCorrectlyForPickup() {
        this.tearsContainer.position = pickedUpEyesPosition.position;
    }

    public override void Disable() {
        DispatchMessage("OnStopHills", null);
        Disable(false);
    }

    public virtual void Disable(bool disableGravity = false) {
		SwitchState(PlayerState.DYING);
		baseBallBat.active = false;
        this.rigidbody2D.velocity = Vector2.zero;
        if(disableGravity) this.rigidbody2D.isKinematic = true;
        this.transform.Find("Head/TearsContainer").active = false;
        canJump = false; //disable jump
        boxCarryComponent.Disable();
        this.GetComponent<PlayerItemComponent>().Disable();
    }

    public override void Enable() {

		SwitchState(PlayerState.RUNNING);
        tearsContainer.active = true;
        this.rigidbody2D.isKinematic = false;
        boxCarryComponent.Enable();
        canJump = true;
        DispatchMessage("OnStartHills", null);
        this.GetComponent<PlayerItemComponent>().Enable();
    }

    public void TurnOnTears() {
        this.transform.Find("Head/TearsContainer").active = true;
    }

    private void LoadDeathScene() {
        MaxLoader.LoadEndScene();
    }

    public void OnBoxDroppingDone() {
        animationController.PlayAnimation("Running Animation");
		SwitchState(PlayerState.RUNNING);
    }

    public Transform GetPickupPoint() {
        return this.transform.Find("PickupPoint");
    }

    public Animation2D GetLips() {
        return lips;
    }

    public override void OnPauseGame() {
        isPaused = true;
        DispatchMessage("OnStopHills", null);
        
        animationController.PauseAnimation();
    }

    public override void OnResumeGame() {
        isPaused = false;
		PreventStoppingBug();
        DispatchMessage("OnStartHills", null);
        
        animationController.ResumeAnimation();
    }

	private void PreventStoppingBug() {
		if(previousState == PlayerState.RUNNING && currentState == PlayerState.STOPPING) {
			animationController.PlayAnimation("Running Animation");
			SwitchState(PlayerState.RUNNING);
		}
	}

	public void OnShowNextWord() {
		talking.PlayRandomSound();
	}

	private void SwitchState(PlayerState newState) {
		previousState = currentState;
		currentState = newState;
	}
}
