using Godot;
using System;

public partial class InputHandler : Node{


	InteractionHandler interactionHandler;
	playerState playerState;
	CamController camController;
	AimManager aimManager;
	AnimationManager animManager;

	//AnimationManager animationManager;
	[Export] playerController playerController;
	[Export] public static float aimSens = .01f;

    public override void _Ready(){
		interactionHandler = (InteractionHandler)GetNode("%InteractionBox");
		playerState = GetNode<playerState>("/root/PlayerState");
		camController = GetNode<CamController>("%MainCamRoot");
		aimManager = GetNode<AimManager>("%aimRoot");
		animManager = GetNode<AnimationManager>("%AnimationManager");
    }
    public override void _Input(InputEvent @event){

		MoveInput();
        
		if(@event.IsActionPressed("Interact")){
			
			interactionHandler.interact();
		}

		if(@event.IsActionPressed("Right Click")){
			if(playerState.canMove && playerState.canLook){
				playerState.IsAiming = true;
				aimManager.setAim();
			}
		}

		if(@event.IsActionReleased("Right Click")){
			if(playerState.canMove && playerState.canLook){
				playerState.IsAiming = false;
				//aimManager.reset();
			}
		}

		//Should animations be directly launched from here? or have some kind of animation launcher class?
		if(@event.IsActionPressed("Drop Lantern")){
			if(playerState.canMove && !playerState.animBusy){
				animManager.pickupDropLantern();
			}

		}

		if(@event.IsActionPressed("Left Click")){
			if(playerState.IsAiming && !playerState.inMenu){
				playerInventory.attack();

			}
		}

		if(@event.IsActionPressed("Reload")){
			playerInventory.reload();
		}
		
    }

	void MoveInput(){
		if(playerState.canMove){
			Vector2	inputDir = Input.GetVector("Left", "Right", "Forward", "Back");
			playerController.setInputDir(inputDir);
		}else{
			playerController.setInputDir(Vector2.Zero);
		}
	}

}
