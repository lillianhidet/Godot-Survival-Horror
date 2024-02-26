using Godot;
using System;

public partial class testController : RigidBody3D{
	public const float Speed = 15.0f;
	public const float lerpVal = .15f;

	int angularAcc = 7;

	Node3D character;
	AnimationTree animTree;

	playerState playerState;

	Node3D horizontal;

	Vector3 velocity;

	Vector2 inputDir;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	//public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready(){
		velocity = new Vector3();
        character = (Node3D)GetNode("Character");
		animTree = (AnimationTree)GetNode("Character/AnimationTree");
		playerState = GetNode<playerState>("/root/PlayerState");
		horizontal = (Node3D)GetNode("MainCamRoot/horizontal");
    }


    public override void _PhysicsProcess(double delta){
		
		float h_rot = horizontal.GlobalTransform.Basis.GetEuler().Y;

		inputDir = Input.GetVector("Left", "Right", "Forward", "Back");
		Vector3 direction = new Vector3(inputDir.X, 0, inputDir.Y).Rotated(Vector3.Up, h_rot).Normalized();
        
		if (direction != Vector3.Zero && playerState.canMove){

			Vector3 rot = new Vector3(0,(float)Mathf.LerpAngle(character.Rotation.Y, Mathf.Atan2(direction.X, direction.Z), delta * angularAcc),0);
			character.Rotation = rot;

			//velocity.X = Mathf.Lerp(velocity.X, direction.X * Speed, lerpVal);
			//velocity.Z = Mathf.Lerp(velocity.Z, direction.Z * Speed, lerpVal);
			velocity.X = Mathf.Lerp(velocity.X, direction.X * Speed, 5f * (float)delta);
			velocity.Z = Mathf.Lerp(velocity.Z, direction.Z * Speed, 5f * (float)delta);
			ApplyCentralForce(velocity);

			


		}else{
			velocity.X = Mathf.Lerp(velocity.X, 0.0f, lerpVal);
			velocity.Z = Mathf.Lerp(velocity.Z, 0.0f, lerpVal);
		}


		
		animTree.Set("parameters/Movement/blend_position", velocity.Length() / Speed);
    }

    public override void _IntegrateForces(PhysicsDirectBodyState3D state){
        if(state.LinearVelocity.Length() > 3){
			state.LinearVelocity = state.LinearVelocity.Normalized()*3;
		}

		if(inputDir.Length() < 0.2){
			float x = (float)Mathf.Lerp(state.LinearVelocity.X, 0, 0.1 );
			float z = (float)Mathf.Lerp(state.LinearVelocity.Z, 0, 0.1 );
			state.LinearVelocity = new Vector3(x,state.LinearVelocity.Y,z);

		}

    }





}
