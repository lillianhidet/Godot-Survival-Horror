using Godot;
using System;
using System.Collections.Generic;



//TODO REFACTOR!!!!!!!!!!!
public partial class AimManager : Node3D
{

	Vector3 fulcrumOrigin;

	[Export] Node3D camPos;
	[Export] float xMoveLimit;
	[Export] float yMoveLimit;

	[Export] float SxRotLimit;
	[Export] float SyRotLimit;

	[Export] float HxRotLimit;
	[Export] float HyRotLimit;

	[Export] Node3D armTargets;
	[Export] Node3D headTarget;

	[Export] Node3D player;
	[Export] Node3D rotation;

	[Export] Node3D spineTarget;

	private List<lookatLerper> lookatLerpers = new List<lookatLerper>();

	float t = 0;

	//This should likely be handeled by the InputHandler
	public override void _Input(InputEvent @event){
		if(playerState.IsAiming){

			if(@event is InputEventMouseMotion){
				InputEventMouseMotion m = (InputEventMouseMotion) @event;

				clearLerpers();
				/*float X = Position.X + -(m.Relative.X * InputHandler.aimSens);
				X = Math.Clamp(X, -xLimit, xLimit);

				float Y = Position.Y +  -(m.Relative.Y * InputHandler.aimSens);
				Y = Math.Clamp(Y, -yLimit, yLimit);

				Vector3 newPos = new Vector3(X, Y, Position.Z );
				Position = newPos;*/


				float HrotY = Math.Clamp(headTarget.RotationDegrees.Y + -(m.Relative.X * 0.12f), -HyRotLimit, HyRotLimit);
				float HrotX = Math.Clamp(headTarget.RotationDegrees.X + (m.Relative.Y * 0.12f), -HxRotLimit, HxRotLimit);
				headTarget.RotationDegrees = new Vector3(HrotX,HrotY,headTarget.RotationDegrees.Z);

				float SrotY = Math.Clamp(spineTarget.RotationDegrees.Y + -(m.Relative.X * 0.12f), -SyRotLimit, SyRotLimit);
				float SrotX = Math.Clamp(spineTarget.RotationDegrees.X + (m.Relative.Y * 0.12f), -SxRotLimit, SxRotLimit);

				spineTarget.RotationDegrees = new Vector3(SrotX,SrotY, spineTarget.RotationDegrees.Z);

				//Convert this to rotation i think, that way everything is consistent
				//float posY = Math.Clamp(armTargets.Position.Y + -(m.Relative.Y * 0.0025f), -yMoveLimit, yMoveLimit);
				//float posX = Math.Clamp(armTargets.Position.X + -(m.Relative.X * 0.0025f), -xMoveLimit, xMoveLimit);
				//armTargets.Position = new Vector3(posX,posY,0);

				float ArotY = Math.Clamp(armTargets.RotationDegrees.Y + -(m.Relative.X * 0.12f), -yMoveLimit, yMoveLimit);
				float ArotX = Math.Clamp(armTargets.RotationDegrees.X + (m.Relative.Y * 0.12f), -xMoveLimit, xMoveLimit);
				armTargets.RotationDegrees = new Vector3(ArotX,ArotY,armTargets.RotationDegrees.Z);

				
			}
		}
	}

	public void setAim(){

		clearLerpers();


		lookatLerper a = new lookatLerper(spineTarget, camPos, 1.0f, SxRotLimit, SyRotLimit);
		lookatLerpers.Add(a);
		AddChild(a);

		lookatLerper l = new lookatLerper(headTarget, camPos, 1.0f, HxRotLimit, HyRotLimit);
		lookatLerpers.Add(l);
		AddChild(l);

		lookatLerper c = new lookatLerper(armTargets, camPos, 1.0f, xMoveLimit, yMoveLimit);
		lookatLerpers.Add(c);
		AddChild(c);

		
		//arghh
		/*float x = armTargets.GlobalPosition.X + (fulcrum.GlobalPosition.X - fulcrumOrigin.X);
		float y = armTargets.GlobalPosition.Y + (fulcrum.GlobalPosition.Y - fulcrumOrigin.Y);
		x = Math.Clamp(x, -xMoveLimit, xMoveLimit);
		y = Math.Clamp(y, -yMoveLimit, yMoveLimit);
		armTargets.GlobalPosition = new Vector3(x,y,0);*/

	}


    public override void _Process(double delta){

		rotation.LookAt(player.GlobalPosition);
		
        if(!playerState.IsAiming && spineTarget.RotationDegrees != Vector3.Zero){

			clearLerpers();

			headTarget.RotationDegrees = headTarget.RotationDegrees.Lerp(Vector3.Zero, t);
			spineTarget.RotationDegrees = spineTarget.RotationDegrees.Lerp(Vector3.Zero, t);
			armTargets.RotationDegrees = armTargets.RotationDegrees.Lerp(Vector3.Zero, t);
			

			t += (float)delta * 0.8f;

		}else{
			t = 0;
		}

		//headTarget.LookAt(fulcrumTarget.GlobalPosition);

		
    }
	


	//
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
