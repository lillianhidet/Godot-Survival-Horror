using Godot;
using System;


public partial class playerController : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float lerpVal = .15f;

	int angularAcc = 7;

	Node3D armature;
	AnimationTree animTree;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready(){

        armature = (Node3D)GetNode("Armature");
		animTree = (AnimationTree)GetNode("AnimationTree");
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y -= gravity * (float)delta;
		}
		
		Node3D n = (Node3D)GetNode("MainCamRoot/horizontal");
		float h_rot = n.GlobalTransform.Basis.GetEuler().Y;
		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Back");
		Vector3 direction = new Vector3(inputDir.X, 0, inputDir.Y).Rotated(Vector3.Up, h_rot).Normalized();
		
		if (direction != Vector3.Zero){
			
		
			Vector3 rot = new Vector3(0,(float)Mathf.LerpAngle(armature.Rotation.Y, Mathf.Atan2(direction.X, direction.Z), delta * angularAcc),0);
			armature.Rotation = rot;


			velocity.X = Mathf.Lerp(velocity.X, direction.X * Speed, lerpVal);
			velocity.Z = Mathf.Lerp(velocity.Z, direction.Z * Speed, lerpVal);
		}
		else{
			velocity.X = Mathf.Lerp(velocity.X, 0.0f, lerpVal);
			velocity.Z = Mathf.Lerp(velocity.Z, 0.0f, lerpVal);
		}

		animTree.Set("parameters/Movement/blend_position", velocity.Length() / Speed);
	
		Velocity = velocity;
		MoveAndSlide();
	}
}
