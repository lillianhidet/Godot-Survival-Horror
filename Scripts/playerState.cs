using Godot;
using System;

public partial class playerState : Node{

	public static bool canMove {get; private set;}
	public static bool canLook {get; private set;}
	public static bool inMenu {get; private set;}

	public static bool animBusy {get; set;}

	public static bool IsAiming;
	public static bool isTransitioning;


    public override void _Ready(){
		Input.MouseMode = Input.MouseModeEnum.Captured;
        canMove = true;
		canLook = true;
		IsAiming = false;
		isTransitioning = false;
		inMenu = false;
    }
    public static void openMenu(){
		Input.MouseMode = Input.MouseModeEnum.Confined;
		canMove = false;
		canLook = false;
		inMenu = true;
	}

	public static void closeMenu(){
		Input.MouseMode = Input.MouseModeEnum.Captured;
		canMove = true;
		canLook = true;
		inMenu = false;

	}

	public static void lockAnim(){

		canMove = false;
		animBusy = true;
	}

	public static void unlockAnim(){

		canMove=true;
		animBusy = false;
	}

	public static void toggleAim(){
		IsAiming = !IsAiming;
	}
}
