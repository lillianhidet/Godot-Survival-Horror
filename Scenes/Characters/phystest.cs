using Godot;
using System;

public partial class phystest : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	
	}

	public override void _PhysicsProcess(double delta){
		
		ApplyTorque(new Vector3(-0.1f,0,0));
	}
}
