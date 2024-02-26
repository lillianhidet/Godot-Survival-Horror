using Godot;
using System;

public partial class playerState : Node{

	public bool canMove {get; private set;}
	public bool canLook {get; private set;}

	public bool IsAiming;


    public override void _Ready(){
		Input.MouseMode = Input.MouseModeEnum.Captured;
        canMove = true;
		canLook = true;
		IsAiming = false;
    }
    public void openMenu(){
		Input.MouseMode = Input.MouseModeEnum.Confined;
		canMove = false;
		canLook = false;
	}

	public void closeMenu(){
		Input.MouseMode = Input.MouseModeEnum.Captured;
		canMove = true;
		canLook = true;

	}

	public void toggleAim(){
		IsAiming = !IsAiming;
	}
}
