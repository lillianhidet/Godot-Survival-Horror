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
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		areaA = GetNode<Area3D>("%A");
		areaB = GetNode<Area3D>("%B");
		areaA.BodyEntered += AEntered;
		areaB.BodyEntered += BEntered;
	}

	public void AEntered(Node3D body){
		if(body.IsInGroup("player")){

			if(AReturnToMain){

				returnToMain();
				
			}else{
				changeCamera(CameraA);
			}

		}
	}

	public void BEntered(Node3D body){
		if(body.IsInGroup("player")){

			if(BReturnToMain){

				returnToMain();
				
			}else{
				changeCamera(CameraB);
			}
			
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
