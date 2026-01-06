using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class itemModelViewerManager : Node
{

	static PackedScene viewerScene;
	static List<itemModelViewer> viewers;

    public override void _Ready(){

        viewerScene = GD.Load<PackedScene>("uid://xas3fq13fsj3");
		viewers = new List<itemModelViewer>();
    }

    public static itemModelViewer newViewer(PackedScene obj){
		itemModelViewer v = (itemModelViewer) viewerScene.Instantiate();
		v.getCamera().Position = new Vector3(3000 * (viewers.Count+1), 3000 * (viewers.Count+1), 3000 * (viewers.Count + 1));
		v.spawnItem(obj);
		viewers.Add(v);

		return v;

	}

	public static void removeViewer(itemModelViewer viewer){
		if(viewers.Contains(viewer)){
			viewer.QueueFree();
			viewers.Remove(viewer);
		}

	}

	public static void clearViewers(){ 
	/*if(viewers.Count > 0 ){
		foreach(itemModelViewer v in viewers) {
				v.QueueFree();
			}*/
		viewers.Clear();
		//}
	}
}
