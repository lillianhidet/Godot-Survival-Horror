using Godot;
using System;
using System.Diagnostics;


public partial class playerController : CharacterBody3D
{
	[Export] public float walkSpeed = 5.0f;
	[Export] public float aimSpeed = 2.5f;

	private static playerController instance;
	public const float lerpVal = .15f;

	RigidBody3D lanternLight;
	Vector2 inputDir;

	int angularAcc = 7;

	Node3D armature;
	AnimationTree animTree;

	AnimationManager animManager;
	playerState playerState;

	Node3D leftHandPos;

	Node3D parent;

	
	public static playerController getInstance(){
		return instance;
	}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready(){
		leftHandPos = (Node3D) GetNode("%LeftHandPos");
		animManager = (AnimationManager)GetNode("%AnimationManager");
        armature = (Node3D)GetNode("Armature");
		animTree = (AnimationTree)GetNode("AnimationTree");
		playerState = GetNode<playerState>("/root/PlayerState");

		if(instance==null){
			instance = this;
		}else {GD.PushError("Attempted to create second instance of singleton");}

		//parent = GetNode("%lanternParent") as Node3D;
	
    }

	public void setInputDir(Vector2 dir){
		inputDir = dir;
	}

    public override void _PhysicsProcess(double delta){


		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y -= gravity * (float)delta;
		}
		
		
		Node3D n = (Node3D)GetNode("MainCamRoot/horizontal");
		float camRot = n.GlobalTransform.Basis.GetEuler().Y;

		

		//This sucks!
		Vector3 turnDir =  new Vector3(inputDir.X, 0, inputDir.Y).Rotated(Vector3.Up, camRot).Normalized();
		Vector3 facing = new Vector3(inputDir.X, 0, inputDir.Y).Rotated(Vector3.Up, armature.GlobalTransform.Basis.GetEuler().Y).Normalized();


			if(!playerState.IsAiming && playerState.canMove){
				
				Velocity = standardMove(inputDir, turnDir, delta, velocity);

			}else{
	
				Velocity = aimMove(inputDir, -facing, delta, velocity);

			}

		
		animManager.walk(Velocity.Length() / walkSpeed);

		//Move this
		
		//parent.GlobalPosition = leftHandPos.GlobalPosition;
		//parent.GlobalRotation = leftHandPos.GlobalRotation;
		


		//lanternLight.ApplyForce(new Vector3(0,0,Math.Abs(velocity.Z) * 5));
		


		MoveAndSlide();

		

		
	}


    private Vector3 aimMove(Vector2 inputDir, Vector3 direction, double delta, Vector3 velocity){

		if(inputDir != Vector2.Zero){

			velocity.X = Mathf.Lerp(velocity.X, direction.X * aimSpeed, lerpVal);
			velocity.Z = Mathf.Lerp(velocity.Z, direction.Z * aimSpeed, lerpVal);

		}else{

			velocity.X = Mathf.Lerp(velocity.X, 0.0f, lerpVal);
			velocity.Z = Mathf.Lerp(velocity.Z, 0.0f, lerpVal);

		}

		return velocity;

	}

	private Vector3 standardMove(Vector2 inputDir, Vector3 direction, double delta, Vector3 velocity){

		if (direction != Vector3.Zero){
			
		
			Vector3 rot = new Vector3(0,(float)Mathf.LerpAngle(armature.Rotation.Y, Mathf.Atan2(direction.X, direction.Z), delta * angularAcc),0);
			armature.Rotation = rot;

			velocity.X = Mathf.Lerp(velocity.X, direction.X * walkSpeed, lerpVal);
			velocity.Z = Mathf.Lerp(velocity.Z, direction.Z * walkSpeed, lerpVal);
			//lantern.ApplyCentralForce(-direction * 2);
		}
		else{
			velocity.X = Mathf.Lerp(velocity.X, 0.0f, lerpVal);
			velocity.Z = Mathf.Lerp(velocity.Z, 0.0f, lerpVal);
		}

		return velocity;
		
	}

}
