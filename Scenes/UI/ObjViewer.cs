using Godot;
using System;

public partial class ObjViewer : Node3D
{
	
	[Export] public Viewport viewport {get; private set;}
	[Export] public Node3D spawnpoint {get; private set;}
	[Export] Texture2D image;

	public void spawnItem(PackedScene item){

		Node3D model = (Node3D) item.Instantiate();
		spawnpoint.AddChild(model);

	}

	public Viewport getViewport(){
		return viewport;
	}

	public NodePath getViewportPath(){

		return viewport.GetPath();

	}
	
}
 