using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class itemModelViewerManager : Node
{

	static PackedScene viewerScene;
	static List<ObjViewer> viewers;

    public override void _Ready(){

        viewerScene = GD.Load<PackedScene>("uid://xas3fq13fsj3");
		viewers = new List<ObjViewer>();
    }

    public static ObjViewer newViewer(PackedScene obj){

		ObjViewer v = (ObjViewer) viewerScene.Instantiate();
		v.spawnItem(obj);
		viewers.Add(v);
		return v;

	}

	public static void removeViewer(ObjViewer viewer){
		if(viewers.Contains(viewer)){
			viewer.QueueFree();
			viewers.Remove(viewer);
		}

	}
}
