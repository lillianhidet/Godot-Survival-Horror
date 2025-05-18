using Godot;
using System;
using System.Threading;

public partial class LanternTest : RigidBody3D
{
	[Export] Node3D handle;

	bool reset = false;

	public bool attached = true;

    public override void _IntegrateForces(PhysicsDirectBodyState3D state){
		//if(moving){
			
			//Rotation = new Vector3(0,0,0);
			//GlobalPosition = handle.GlobalPosition;
			//AngularVelocity = new Vector3(0,0,0);
			//state.AngularVelocity = new Vector3(0,0,0);
			//ResetPhysicsInterpolation();

		//}else{
			if(!attached){
        	GlobalPosition = handle.GlobalPosition;
			AngularVelocity = state.AngularVelocity;
	
			ResetPhysicsInterpolation();
			}else{
				GlobalPosition = handle.GlobalPosition;
				GlobalRotation = handle.GlobalRotation;
			}
		//}
	
		
		
    }

    public override void _PhysicsProcess(double delta)
    {
       	if(attached){
			GlobalPosition = handle.GlobalPosition;
			GlobalRotation = handle.GlobalRotation;
			
    	}
	
	}

	



}
