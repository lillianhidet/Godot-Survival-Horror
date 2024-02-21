using Godot;
using System;

public partial class InputHandler : Node{


	InteractionHandler interactionHandler;

    public override void _Ready(){
		interactionHandler = (InteractionHandler)GetNode("%InteractionBox");
    }
    public override void _Input(InputEvent @event){
        
		if(@event.IsActionPressed("Interact")){
			interactionHandler.interact();
		}


    }
}
