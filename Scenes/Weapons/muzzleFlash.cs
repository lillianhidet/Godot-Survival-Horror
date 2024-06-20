using Godot;
using System;

public partial class muzzleFlash : Node3D{
	public void flash(float time){
		this.Visible = true;	
		GetTree().CreateTimer(time).Timeout += () => hide();

	}

	public void hide(){
		this.Visible = false;
	}
}

//GetTree().CreateTimer(timeToOpenEyes).Timeout += () => openEyes();