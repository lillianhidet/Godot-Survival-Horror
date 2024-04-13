using Godot;
using System;

public partial class Blink : MeshInstance3D
{

	[Export] Material open;
	[Export] Material close;
	[Export] float minTimeBeforeBlink = 1.5f;
	[Export] float maxTimeBeforeBlink = 4f;

	[Export] float timeToOpenEyes = 0.1f;

	bool blinking = false;
	RandomNumberGenerator rng;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		rng = new RandomNumberGenerator();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta){

		float random = rng.RandfRange(minTimeBeforeBlink, maxTimeBeforeBlink);

		if(!blinking){
			blinking = true;
		GetTree().CreateTimer(random).Timeout += () => closeEyes();
		}


	}

	public void closeEyes(){
		
		SetSurfaceOverrideMaterial(1,close);
		GetTree().CreateTimer(timeToOpenEyes).Timeout += () => openEyes();


	}

	private void openEyes(){
		
		SetSurfaceOverrideMaterial(1,open);
		blinking = false;
	}
}
