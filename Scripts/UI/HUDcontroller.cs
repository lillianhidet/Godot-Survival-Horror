using Godot;
using System;

public partial class HUDcontroller : Control
{
	Control reticle;
	public override void _Ready(){

		reticle = GetNode<Control>("Reticle");

	}

	

	public void adjustReticle(Vector2 adjustment){

		reticle.Position += adjustment;

	}

	public void resetReticle(){

		reticle.Position = new Vector2(10,10);

	}

}
