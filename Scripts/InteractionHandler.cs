using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class InteractionHandler : Area3D{

	Node currentTarget;

	List<interactable> targets = new List<interactable>();

	interactable closest;

	private static InteractionHandler instance;

	[Export] Marker3D position;

	public override void _Ready(){
		if(instance==null){
			instance = this;
		}else {GD.PushError("Attempted to create second instance of singleton");}
	}

	public static InteractionHandler getInstance(){
		return instance;
	}

	void areaEntered(Area3D area){
		
		if(area.IsInGroup("Interactable")){
			//currentTarget = area.GetParent();
			targets.Add((interactable) area.GetParent());

		}

	}

	void areaExited(Area3D area){

		if(area.IsInGroup("Interactable")){
			//if(currentTarget == area.GetParent()){

				//currentTarget = null;

			//}
			targets.Remove((interactable) area.GetParent());

		}

	}


	public void interact(){

		getClosest();
		//currentTarget != null && 
		if(!playerState.inMenu && closest != null){
			
			//meh, may as well just get class ref earlier? beccause godot hates me and this has to be two lines
			//interactable i = (interactable)currentTarget;
			//i.interact();
			
			closest.interact();

		}

	}

	public void getClosest(){

		foreach (interactable i in targets){
			if(closest == null){closest = i;}

			if(IsInstanceValid(closest)){

				if(IsInstanceValid(i)){

					if(position.GlobalPosition.DistanceTo(i.GlobalPosition) < position.GlobalPosition.DistanceTo(closest.GlobalPosition)){
						closest = i;
					}

				}else{targets.Remove(i);}

			}else{

				targets.Remove(closest);
				closest = null;
				getClosest();

			}
		}
		
		if(targets.Count == 0){closest = null;}
	}
}
