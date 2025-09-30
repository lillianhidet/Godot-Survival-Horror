using Godot;
using System;

public partial class CameraTransition : Node3D
{
	[ExportGroup("AreaA")]
	[Export] Camera3D CameraA;
	[Export] bool AReturnToMain = false;
	[ExportGroup("AreaB")]
	[Export] Camera3D CameraB;
	[Export] bool BReturnToMain = false;

	Area3D areaA;
	Area3D areaB;

	bool EnteredA = false; 
	bool EnteredB = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		areaA = GetNode<Area3D>("%A");
		areaB = GetNode<Area3D>("%B");

		areaA.BodyEntered += AEntered;
		areaB.BodyEntered += BEntered;

		areaA.BodyExited += AExited;
		areaB.BodyExited += BExited;
	}

	public void AEntered(Node3D body){
		if(body.IsInGroup("player") && !EnteredB){
			EnteredA = true;

			if(AReturnToMain){

				returnToMain();
				
			}else{
				changeCamera(CameraA);
			}

		}
	}

	public void AExited(Node3D body) {
        if (body.IsInGroup("player")) {
			EnteredB = false;
        }
	}


    public void BEntered(Node3D body){
		if(body.IsInGroup("player") && !EnteredA){
            EnteredB = true;
            if (BReturnToMain){

				returnToMain();
				
			}else{
				changeCamera(CameraB);
			}
			
		}
	}

    public void BExited(Node3D body) {
        if (body.IsInGroup("player")) {
			EnteredA = false;
        }
    }

    private void changeCamera(Camera3D target){

		if(playerState.currentCamera != target){

			playerState.currentCamera.Current = false;
			playerState.currentCamera = target;
			playerState.staticCam = true;
			target.Current = true;

		}
	}

	private void returnToMain(){
		if(playerState.currentCamera != MainCamera.instance){
			playerState.currentCamera.Current = false;
			playerState.currentCamera = MainCamera.instance;
			playerState.staticCam = false;
		}
		
	}
}
