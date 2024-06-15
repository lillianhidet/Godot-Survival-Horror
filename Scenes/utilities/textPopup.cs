using Godot;
using System;

public partial class textPopup : Node
{	
	[Export] string toDisplay;
	[Export] int lifespan;

	public void entered(Area3D area){
		GD.Print("yes");
		if(area.IsInGroup("player")){
			hudManager.displayhudtext(toDisplay, lifespan);
			QueueFree();
		}
	}
}
