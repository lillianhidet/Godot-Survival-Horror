using Godot;
using System;

public partial class ColourManager : Node
{
	public static ColourManager instance { get; private set; }

    [Export] public Color worlditem_yesCol;
    [Export] public Color worlditem_noCol;
    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree(){
		if(instance != null){
			this.QueueFree();
		}
		instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
