using Godot;
using System;

public partial class FakePlayer : RigidBody3D
{
    public override void _IntegrateForces(PhysicsDirectBodyState3D state)
    {
    

			state.ApplyForce(Vector3.Forward * 10, GlobalTransform.Origin);

    }
}
