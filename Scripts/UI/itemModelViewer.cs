using Godot;
using System;

public partial class itemModelViewer : Node3D
{
	
	[Export] public Viewport viewport {get; private set;}
	[Export] public Node3D spawnpoint {get; private set;}

	[Export] public Node3D camera {get; private set;}

	[Export] public float rotationSpeed = 1f;
	
	private Vector2 lastMousePos;

	public enum rotateMode{
		none,
		mouse,
		auto
	}

	private rotateMode rotationMode;


	public void spawnItem(PackedScene item){
	
		Node model = item.Instantiate();
		spawnpoint.AddChild(model);
		
	}

	public void setItemSize(float size){

		spawnpoint.Scale = new Vector3(size, size, size);
		
	}

	public void lerpSize(float start, float end){

		itemModelViewerLerper lerper = new itemModelViewerLerper(spawnpoint, start, end);
		GetTree().Root.AddChild(lerper);
		
	}

	public Node3D getCamera(){

		return camera;
	}

	public Viewport getViewport(){
		return viewport;
	}

	public NodePath getViewportPath(){

		return viewport.GetPath();

	}

	public void setRotationMode(rotateMode mode){
		rotationMode = mode;
	}

    public override void _Process(double delta){

		if(rotationMode != rotateMode.none){
			if(rotationMode == rotateMode.mouse){
				mouseRotate(delta);
			}else{
				//spawnpoint.Rotate(new Vector3(0,1,0), spawnpoint.Rotation.Y + (rotationSpeed * (float) delta));
				spawnpoint.RotateY(rotationSpeed * (float) delta);
			}

		}
		
    }

	private void mouseRotate(double delta){

		Vector2 newMousePos = GetViewport().GetMousePosition();

		if (Input.IsActionPressed("Left Click"))
		{
			float dif = newMousePos.X - lastMousePos.X;

			spawnpoint.Rotate(new Vector3(0, 1, 0), (dif * (float)delta) / 2);
		}else{ 
			spawnpoint.RotateY(rotationSpeed * (float) delta);
		}

		lastMousePos = newMousePos;


	}

	public void resetRotation(){
		spawnpoint.Rotation = Vector3.Zero;
	}



}
 