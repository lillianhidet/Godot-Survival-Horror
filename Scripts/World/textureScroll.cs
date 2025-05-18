using Godot;
using System;

public partial class textureScroll : MeshInstance3D{


	[Export] public float speed = .05f;
	[Export] public int x = 0;
	[Export] public int y = 0;
	[Export] public bool moveMesh = false;

	
	BaseMaterial3D m;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		m = (BaseMaterial3D) GetSurfaceOverrideMaterial(0);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){

		float xMove = ((float) delta * speed) * x;
		float yMove = ((float) delta * speed) * y;

		
		m.Uv1Offset = new Vector3(m.Uv1Offset.X + xMove, m.Uv1Offset.Y + yMove, m.Uv1Offset.Z ) ;

		

		}

	}

