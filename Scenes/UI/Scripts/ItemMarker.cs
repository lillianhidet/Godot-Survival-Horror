using Godot;
using System;

public partial class ItemMarker : TextureRect{


	public bool focus = false;

	public bool fadingOut = false;

	public bool fadingIn = false;

	public bool deleting = false;

	public itemMarkerLerper lerper;


	public void addLerper(itemMarkerLerper lerp){
		clearLerper();
		lerper = lerp;
		AddChild(lerp);
	}



	private void clearLerper(){
		if(lerper != null && IsInstanceValid(lerper)){
			lerper.QueueFree();
			lerper = null;
		}
	}
}
