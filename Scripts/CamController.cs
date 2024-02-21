using Godot;
using System;
using System.Numerics;

public partial class CamController : Node3D{

	float camrot_h = 0f;
	float camrot_v = 0f;

	float sens = .1f;
	float t = 0.0f;

	Node3D horizontal;
	Node3D vertical;
	Camera3D mainCam;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		horizontal = (Node3D) GetNode("horizontal");
		vertical = (Node3D) GetNode("horizontal/vertical");
		mainCam = (Camera3D) GetNode("horizontal/vertical/mainCamPos/Camera3D");

		Input.MouseMode = Input.MouseModeEnum.Captured;

	}

    public override void _Input(InputEvent @event){
        
		if(@event is InputEventMouseMotion){
			InputEventMouseMotion m = (InputEventMouseMotion) @event;

			camrot_h += - m.Relative.X * sens;
			camrot_v += - m.Relative.Y * sens;



		}
    }

    public override void _PhysicsProcess(double delta){
		Godot.Vector3 hor = new Godot.Vector3(horizontal.RotationDegrees.X, camrot_h, horizontal.RotationDegrees.Z);
		horizontal.RotationDegrees = hor;

		Godot.Vector3 vert = new Godot.Vector3(camrot_v, vertical.RotationDegrees.Y, vertical.RotationDegrees.Z);
		vertical.RotationDegrees = vert;
    }


}
