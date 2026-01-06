using Godot;
using System;

public partial class TrainSpawner : Node3D{

	float wait;
	float elapsed = 0;

	Node3D currentTrain;
	[Export] float trainSpeed = 3f;
	[Export] PackedScene train;
 
	RandomNumberGenerator rng = new RandomNumberGenerator();



	//Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		wait = rng.RandfRange(10,20);
		newTrain();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		currentTrain.GlobalPosition = new Vector3(currentTrain.GlobalPosition.X, currentTrain.GlobalPosition.Y, currentTrain.GlobalPosition.Z - trainSpeed);

		elapsed += (float)delta;

		if (elapsed >= wait){
			elapsed = 0;
			wait = rng.RandfRange(5,10);
			newTrain();
		}

	}

	private void newTrain(){
		if(currentTrain != null){
		currentTrain.QueueFree();
		}		
		currentTrain = (Node3D)train.Instantiate();
		AddChild(currentTrain);

	}
}
