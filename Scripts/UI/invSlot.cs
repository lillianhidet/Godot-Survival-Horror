using Godot;
using System;

public partial class invSlot : Control
{
	[Export] public float targetOpacity {get;set;}
	public invSlotItem inSlot {get; set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
