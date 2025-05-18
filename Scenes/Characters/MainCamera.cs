using Godot;
using System;

public partial class MainCamera : Camera3D
{
	public static MainCamera instance {get; private set;}

    public override void _Ready(){
        instance = this;
		playerState.currentCamera = instance;
    }



}
