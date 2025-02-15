using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TestInput : Node
{


	InteractionHandler interactionHandler;
	playerState playerState;
	CamController camController;
	AimManager aimManager;
	AnimationManager animManager;

	playerController controller;

	Inventory_UI ui;

	bool instantiated = false;

	//Create a file that stores all "settings" values like this
	public float aimSens = .01f;

    public override void _Ready()
    {
        
    }

	private void setFields(){
		ui = GetNode<Inventory_UI>("../Menu");
		playerState = GetNode<playerState>("/root/PlayerState");

		interactionHandler = InteractionHandler.getInstance();
		camController = CamController.getInstance();
		aimManager = AimManager.getInstance();
		animManager = AnimationManager.getInstance();
		controller = playerController.getInstance();

		instantiated = true;

	}

    public override void _Input(InputEvent @event){

		//Sucks! But solves the problem!
		if(!instantiated){
			setFields();
		}

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


		if(@event.IsActionPressed("Menu")){
			if(!playerState.inMenu){
				playerState.openMenu();
				List<InventoryItem> items = playerInventory.weapons.ToList<InventoryItem>();
				ui.open(items);
			}
		}
		if(@event.IsActionPressed("Right")){
			if(playerState.inMenu){
				ui.moveSelectionRight();
			}

		}

		if(@event.IsActionPressed("Left")){
			if(playerState.inMenu){
				ui.moveSelectionLeft();
			}
		}

		if(@event.IsActionPressed("Close")){
			if(playerState.inMenu){
				if(ui.isOpen){
					ui.exit();
					playerState.closeMenu();
				}
			}
		}
	}



	void MoveInput(){
		if(playerState.canMove){
			Vector2	inputDir = Input.GetVector("Left", "Right", "Forward", "Back");
			controller.setInputDir(inputDir);
		}else{
			controller.setInputDir(Vector2.Zero);
		}
	}

}
