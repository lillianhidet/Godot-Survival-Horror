using Godot;
using System;


//Still very messy, needs another refactor pass (a lot better than what it was!)


public partial class CamController : Node3D{


	private static CamController instance;
	private float camrot_h = 0f;
	private float camrot_v = 0f;

	private float lerpVal = 0;
	[Export] public float transitionSpeed = 3f;
	[Export] public float mainSens = .1f;
	[Export] public int maxRot = 15;
	[Export] public int minRot = -10;

	[Export] public float mainFOV = 75;
	[Export] public float aimFOV = 50;

 
	private float sens;

	Node3D Mhorizontal;
	Node3D Mvertical;
	[Export] Camera3D mainCam;

	[Export] Node3D rightAimTarget;
	[Export] Node3D leftAimTarget;
	[Export] Node3D mainTarget;

	[Export] SpringArm3D springArm;

	[Export] Node3D Armature;

	Vector3 mainCamCurrent;

	Node3D targetNode;

	bool goingOut = false;

	bool atDestination = true;
	


	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		mainCam.Fov = mainFOV;
		Mhorizontal = (Node3D) GetNode("horizontal");
		Mvertical = (Node3D) GetNode("horizontal/vertical");

		springArm.GlobalPosition = mainTarget.GlobalPosition;

		sens = mainSens;

		if(instance==null){
			instance = this;
		}else {GD.PushError("Attempted to create second instance of singleton");}

	}

	public static CamController getInstance(){
		return instance;
	}

    public override void _Input(InputEvent @event){
 
		if(@event is InputEventMouseMotion){
			if(playerState.canLook && !playerState.IsAiming){

				InputEventMouseMotion m = (InputEventMouseMotion) @event;
				
				camrot_h += - m.Relative.X * sens;
				camrot_v += - m.Relative.Y * sens;
			}

		}
	}

    public override void _Process(double delta){
		transitionAim(delta);
		processMainCam();


    }


	public void camOut(){
		mainCamCurrent = springArm.GlobalPosition;
		lerpVal = 0;
		goingOut = true;
		atDestination = false;
		
		//Ugh, this needs to be based relative to the rotation of the armature
		float rot = Mhorizontal.RotationDegrees.Y % 360;
		rot = rot - (Armature.RotationDegrees.Y % 360);

		//mess of an if
		if((rot > 180 && rot < 360 ) || (rot < 0 && rot > -180) || (rot < -360)){
			targetNode = leftAimTarget;
		}else{
			targetNode = rightAimTarget;
		}
		
	}

	public  void camIn(){
		mainCamCurrent = springArm.GlobalPosition;
		lerpVal = 0;
		goingOut = false;
		atDestination = false;
		
		
	}


	public void transitionAim(double delta){
	if(!atDestination){
		if(goingOut){
			//This transition should be its own function
			//Transition to aiming state
			lerpVal += transitionSpeed * (float)delta;

			if(lerpVal < 1){
				
				springArm.GlobalPosition = mainCamCurrent.Lerp(targetNode.GlobalPosition, lerpVal);
				mainCam.Fov = Mathf.Lerp(mainCam.Fov, aimFOV, lerpVal);

			}else{
				atDestination = true;
				
			}
				
			}else{

				lerpVal += transitionSpeed * (float)delta;

				if(lerpVal < 1){

					springArm.GlobalPosition = mainCamCurrent.Lerp(mainTarget.GlobalPosition, lerpVal);
                    mainCam.Fov = Mathf.Lerp(mainCam.Fov, mainFOV, lerpVal);

                } else{
					atDestination = true;
					
				}

			}

	}

	}

	void processMainCam(){
		camrot_v = Mathf.Clamp(camrot_v, minRot, maxRot);

		Vector3 hor = new Vector3(Mhorizontal.RotationDegrees.X, camrot_h, Mhorizontal.RotationDegrees.Z);
		Mhorizontal.RotationDegrees = hor;

		

		Vector3 vert = new Vector3(camrot_v, Mvertical.RotationDegrees.Y, Mvertical.RotationDegrees.Z);
		Mvertical.RotationDegrees = vert;

	}



}