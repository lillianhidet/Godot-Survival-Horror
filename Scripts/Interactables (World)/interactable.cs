using Godot;
using System;

public partial class interactable : Node3D{

	//[Export] bool focused = false;
	//[Export] TextureRect marker;

	//public bool markerActive;

	

	//[Export] string name;
	/*private Action interactionTarget;

	public void setInteractionTarget(Action action){

		interactionTarget = action;

	}

	public void interact(){

		if(interactionTarget!= null){
			interactionTarget();
		}else{
			GD.PushError("Unassigned interaction target");
		}
	}*/

	public virtual void interact(){}

	
}
