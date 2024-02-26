using Godot;
using System;
using System.Reflection.Emit;

public partial class lantern : RigidBody3D
{

	Vector3 pos;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){

		pos = Position;

	}

    public override void _IntegrateForces(PhysicsDirectBodyState3D state){
		//state.LinearVelocity = new Vector3(0,0,0);
		GD.Print(state.Transform);

    }

}
