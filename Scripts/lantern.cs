using Godot;
using System;

public partial class lantern : RigidBody3D
{
	[Export] Node3D position;
    // Called when the node enters the scene tree for the first time.

    // Called every frame. 'delta' is the elapsed time since the previous frame.


    public override void _IntegrateForces(PhysicsDirectBodyState3D state)
    {
        Transform3D t = state.Transform;
        
        t.Origin.X = position.Position.X;
        t.Origin.Y = position.Position.Y;
        t.Origin.Z = position.Position.Z;
        state.Transform = t;
        
        
    }




}
