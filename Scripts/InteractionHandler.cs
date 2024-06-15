using Godot;
using System;
using System.Diagnostics;

public partial class InteractionHandler : Node{

	Node currentTarget;
	void areaEntered(Area3D area){
		
		if(area.GetTree().HasGroup("Interactable")){

			currentTarget = area.GetParent();

		}

	}

	void areaExited(Area3D area){

		if(area.GetTree().HasGroup("Interactable")){
			if(currentTarget == area.GetParent()){

				currentTarget = null;

			}

		}

	}


	public void interact(){
		if(currentTarget != null){
			
			//meh, may as well just get class ref earlier? beccause godot hates me and this has to be two lines
			interactable i = (interactable)currentTarget;
			i.interact();

		}

	}
}
