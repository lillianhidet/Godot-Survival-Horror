using Godot;
using System;
using System.Collections.Generic;



//TODO REFACTOR!!!!!!!!!!!
public partial class AimManager : Node3D{

	private static AimManager instance;

	[Export] Node3D camPos;
	[Export] float xMoveLimit;
	[Export] float yMoveLimit;

	[Export] float SxRotLimit;
	[Export] float SyRotLimit;

	[Export] Node3D armTargets;

	[Export] Node3D spineTarget;

	private List<lookatLerper> lookatLerpers = new List<lookatLerper>();



    public override void _Ready(){
        if(instance==null){
			instance = this;
		}else {GD.PushError("Attempted to create second instance of singleton");}
    }

	public static AimManager getInstance(){
		return instance;
	}

    //This should likely be handeled by the InputHandler
    public override void _Input(InputEvent @event){


		if(playerState.IsAiming){

			if(@event is InputEventMouseMotion){
				InputEventMouseMotion m = (InputEventMouseMotion) @event;

				clearLerpers();



				float SrotY = Math.Clamp(spineTarget.RotationDegrees.Y + -(m.Relative.X * 0.12f), -SyRotLimit, SyRotLimit);
				float SrotX = Math.Clamp(spineTarget.RotationDegrees.X + (m.Relative.Y * 0.12f), -SxRotLimit, SxRotLimit);

				spineTarget.RotationDegrees = new Vector3(SrotX,SrotY, spineTarget.RotationDegrees.Z);



				float ArotY = Math.Clamp(armTargets.RotationDegrees.Y + -(m.Relative.X * 0.12f), -yMoveLimit, yMoveLimit);
				float ArotX = Math.Clamp(armTargets.RotationDegrees.X + (m.Relative.Y * 0.12f), -xMoveLimit, xMoveLimit);
				armTargets.RotationDegrees = new Vector3(ArotX,ArotY,armTargets.RotationDegrees.Z);


				
			}
		}
	}

	public void setAim(){
		
		clearLerpers();


		lookatLerper a = new lookatLerper(spineTarget, camPos.GlobalPosition, 3.0f, SxRotLimit, SyRotLimit);
		lookatLerpers.Add(a);
		AddChild(a);

		lookatLerper c = new lookatLerper(armTargets, camPos.GlobalPosition, 3.0f, xMoveLimit, yMoveLimit);
		lookatLerpers.Add(c);
		AddChild(c);

	}

	public void stopAim(){

        clearLerpers();

		
		lookatLerper a = new lookatLerper(spineTarget, Vector3.Zero, 3.0f, SxRotLimit, SyRotLimit);
		lookatLerpers.Add(a);
		AddChild(a);

		lookatLerper c = new lookatLerper(armTargets, Vector3.Zero, 3.0f, xMoveLimit, yMoveLimit);
		lookatLerpers.Add(c);
		AddChild(c);
		


    }


	
	void clearLerpers(){
		//This prevents enum errors
		if(lookatLerpers.Count > 0){
			foreach(lookatLerper l in lookatLerpers){
				l.scheduleDeath();
			}
			lookatLerpers.Clear();
		}
	}
}
