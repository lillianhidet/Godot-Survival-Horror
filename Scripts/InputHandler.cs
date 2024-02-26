using Godot;
using System;

public partial class InputHandler : Node{


	InteractionHandler interactionHandler;
	playerState playerState;
	CamController camController;

    public override void _Ready(){
		interactionHandler = (InteractionHandler)GetNode("%InteractionBox");
		playerState = GetNode<playerState>("/root/PlayerState");
		camController = GetNode<CamController>("%MainCamRoot");
    }
    public override void _Input(InputEvent @event){
        
		if(@event.IsActionPressed("Interact")){
			interactionHandler.interact();
		}

		if(@event.IsActionPressed("Right Click")){
			if(playerState.canMove && playerState.canLook){
				camController.toggleAim();

			}


		}
		

    }

}
