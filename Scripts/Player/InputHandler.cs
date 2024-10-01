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
				camController.camOut();
			}
			
		}

		if(@event.IsActionReleased("Right Click")){
			if(playerState.canMove && playerState.canLook){
				playerState.IsAiming = false;
				aimManager.stopAim();
				camController.camIn();
				//aimManager.reset();
			}
			

		}

	

		
		if(@event.IsActionPressed("Drop Lantern")){
			if(playerState.canMove && !playerState.animBusy){
				animManager.takeHolsterLantern();
				
			}

		}

		if(@event.IsActionPressed("Left Click")){
			if(playerState.IsAiming && !playerState.inMenu){
				playerInventory.attack();

			}
		}

		//Definitely move some of this, how to best determine what type of animation to play?
		//haha this is soo bad
		if(@event.IsActionPressed("Reload")){
			if(playerState.canLook && playerState.canMove && !playerInventory.holdingLantern 
			&& playerInventory.currentlyHeld.itemType == heldItem.type.ranged){
				
				rangedWeapon wep = (rangedWeapon) playerInventory.currentlyHeld;

				if(wep.getLoaded() < wep.getCapacity()){
					//For now pistol is the only animaion
					animManager.startPistolReload();
				}

				
				//playerInventory.reload();
			}
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
