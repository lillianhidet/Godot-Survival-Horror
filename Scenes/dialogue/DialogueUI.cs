using Godot;
using System;

public partial class DialogueUI : Control
{
	public static DialogueUI Instance { get; set; }
    [Export] public PackedScene choiceButton;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Instance = this;
		CanvasLayer c = GetNode<CanvasLayer>("%Canvas");
		c.Visible = false;
	}

	
}
