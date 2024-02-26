using Godot;
using System;


public partial class CamController : Node3D{

	float camrot_h = 0f;
	float camrot_v = 0f;
	private float lerpVal = 0;
	[Export] public float transitionSpeed = 0.4f;
	float sens = .1f;
	float t = 0.0f;

	private bool transitioning = false;
	Node3D Mhorizontal;
	Node3D Mvertical;
	Camera3D mainCam;

	Node3D aimCamPos;
	Node3D mainCamPos;

	playerState playerState;

	//Refactor
	private float storedCamrot_h;
	private float storedCamrot_v;
	


	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		Mhorizontal = (Node3D) GetNode("horizontal");
		Mvertical = (Node3D) GetNode("horizontal/vertical");
		mainCam = (Camera3D) GetNode("horizontal/vertical/mainCamPos/Camera3D");
		playerState = GetNode<playerState>("/root/PlayerState");

		aimCamPos = GetNode<Node3D>("aimCamPos");
		mainCamPos = GetNode<Node3D>("horizontal/vertical/mainCamPos");
	}

    public override void _Input(InputEvent @event){
        
		if(@event is InputEventMouseMotion){
			if(playerState.canLook && !transitioning){

				InputEventMouseMotion m = (InputEventMouseMotion) @event;

				camrot_h += - m.Relative.X * sens;
				camrot_v += - m.Relative.Y * sens;

			}
		}

	}

    public override void _PhysicsProcess(double delta){
		GD.Print(Mhorizontal.RotationDegrees.Y);
		if(!transitioning){

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
		if(!transitioning){
			transitioning = true;
		}
	}

	//TODO - Refactor!!!!
	public void transitionAim(double delta){

		if(!playerState.IsAiming){
			//This transition should be its own function
			if(lerpVal < 1){

				lerpVal += transitionSpeed * (float)delta;

				mainCam.GlobalTransform = mainCamPos.GlobalTransform.InterpolateWith(aimCamPos.GlobalTransform, lerpVal);

			}else{
				mainCam.Reparent(aimCamPos);
				transitioning = false;
				
				//Don't like this
				playerState.IsAiming = true;
				lerpVal = 0;

				storedCamrot_h = camrot_h;
				storedCamrot_v = camrot_v;

				//Refactor
				camrot_h = aimCamPos.Position.X;
				camrot_v = aimCamPos.Position.Y;

			}

		}else{

			if(lerpVal < 1){
				
				lerpVal += transitionSpeed * (float)delta;

				mainCam.GlobalTransform = aimCamPos.GlobalTransform.InterpolateWith(mainCamPos.GlobalTransform, lerpVal);

			}else{

				mainCam.Reparent(mainCamPos);
				transitioning = false;

				playerState.IsAiming = false;
				lerpVal = 0;

				//Refactor
				camrot_h = storedCamrot_h;
				camrot_v = storedCamrot_v;

			}

		}



	}

	void processMainCam(){

		Vector3 hor = new Vector3(Mhorizontal.RotationDegrees.X, camrot_h, Mhorizontal.RotationDegrees.Z);
		Mhorizontal.RotationDegrees = hor;
		

		Vector3 vert = new Vector3(camrot_v, Mvertical.RotationDegrees.Y, Mvertical.RotationDegrees.Z);
		Mvertical.RotationDegrees = vert;

	}

	void processAimCam(){

		camrot_h = Mathf.Clamp(camrot_h, -1, 1);
		camrot_v = Mathf.Clamp(camrot_v, 2, 4);

		Vector3 pos = new Vector3(camrot_h, camrot_v, aimCamPos.Position.Z);

		aimCamPos.Position = pos;


	}


}