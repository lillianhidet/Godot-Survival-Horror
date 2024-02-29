using Godot;
using System;

//Todo - reset positions after transitioning out of state,
//Aim cam pos not rotating to remain behind player (done)
//Refactor!! This design stinks!!!!!!!!


public partial class CamController : Node3D{

	private float camrot_h = 0f;
	private float camrot_v = 0f;

	private float horLast = 0f;
	private float lerpVal = 0;
	[Export] public float transitionSpeed = 0.8f;
	[Export] public float mainSens = .1f;
	[Export] float aimSens = .01f;

	private float sens;

	Node3D Mhorizontal;
	Node3D Mvertical;
	Camera3D mainCam;

	Node3D aimCamPos;
	Node3D mainCamPos;

	Node3D armature;

	playerState playerState;
	HUDcontroller hud;

	


	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		Mhorizontal = (Node3D) GetNode("horizontal");
		Mvertical = (Node3D) GetNode("horizontal/vertical");
		mainCam = (Camera3D) GetNode("horizontal/vertical/mainCamPos/Camera3D");
		playerState = GetNode<playerState>("/root/PlayerState");
		armature = GetNode<Node3D>("%Armature");
		aimCamPos = GetNode<Node3D>("%aimCamPos");
		mainCamPos = GetNode<Node3D>("horizontal/vertical/mainCamPos");
		hud = GetNode<HUDcontroller>("/root/World/%HUD");

		sens = mainSens;

	}

    public override void _Input(InputEvent @event){
        
		if(@event is InputEventMouseMotion){
			if(playerState.canLook && !playerState.isTransitioning){

				InputEventMouseMotion m = (InputEventMouseMotion) @event;
				
				camrot_h += - m.Relative.X * sens;
				camrot_v += - m.Relative.Y * sens;

				


			}
		}

	}

    public override void _PhysicsProcess(double delta){
		if(!playerState.isTransitioning){

			if(!playerState.IsAiming){
				processMainCam();
			}else{
				processAimCam();
			}

		}else{
			
			transitionAim(delta);
		}

    }

	public void toggleAim(){
		if(!playerState.isTransitioning){
			playerState.isTransitioning = true;
		}
	}

	//TODO - Refactor!!!!
	public void transitionAim(double delta){

		if(!playerState.IsAiming){
			//This transition should be its own function
			//Transition to aiming state
			if(lerpVal < 1){

				lerpVal += transitionSpeed * (float)delta;

				mainCam.GlobalTransform = mainCamPos.GlobalTransform.InterpolateWith(aimCamPos.GlobalTransform, lerpVal);

			}else{
				mainCam.Reparent(aimCamPos);
				playerState.isTransitioning = false;
				
				//Don't like this
				playerState.IsAiming = true;
				lerpVal = 0;

				sens = aimSens;
				//Refactor
				camrot_h = aimCamPos.Position.X;
				camrot_v = aimCamPos.Position.Y;

			}

		}else{
			//Transition to regular state

			Mhorizontal.Rotation = armature.Rotation;
			Mvertical.Rotation = new Vector3(0,0,0);
			
			if(lerpVal < 1){
				
				lerpVal += transitionSpeed * (float)delta;
				GD.Print(lerpVal);

				mainCam.GlobalTransform = aimCamPos.GlobalTransform.InterpolateWith(mainCamPos.GlobalTransform, lerpVal);

			}else{

				mainCam.Reparent(mainCamPos);
				playerState.isTransitioning = false;
				sens = mainSens;

				playerState.IsAiming = false;
				lerpVal = 0;

				//Refactor
				camrot_h = armature.RotationDegrees.Y;
				camrot_v = 0;

				

			}

		}



	}

	void processMainCam(){
		camrot_v = Mathf.Clamp(camrot_v, -25, 10);

		Vector3 hor = new Vector3(Mhorizontal.RotationDegrees.X, camrot_h, Mhorizontal.RotationDegrees.Z);
		Mhorizontal.RotationDegrees = hor;
		

		Vector3 vert = new Vector3(camrot_v, Mvertical.RotationDegrees.Y, Mvertical.RotationDegrees.Z);
		Mvertical.RotationDegrees = vert;

	}

	void processAimCam(){
		

		camrot_h = Mathf.Clamp(camrot_h, -2, 2);
		camrot_v = Mathf.Clamp(camrot_v, 3, 4);


		hud.adjustReticle(new Vector2((horLast - camrot_h) * 20, 0));

		Vector3 pos = new Vector3(camrot_h, camrot_v, aimCamPos.Position.Z);

		aimCamPos.Position = pos;

		horLast = camrot_h;


	}


}