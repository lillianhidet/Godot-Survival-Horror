using Godot;
using System;

public partial class DialogueUI : CanvasLayer
{
	public static DialogueUI Instance { get; set; }
	[Export] public PackedScene choiceButton;
	public DialogueHandler current;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Instance = this;

		Visible = false;
	}

	
}
