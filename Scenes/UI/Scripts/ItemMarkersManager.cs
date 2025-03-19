using Godot;
using System;
using System.Collections.Generic;

public partial class ItemMarkersManager : Control{

	[Export] PackedScene itemMarker;

	Dictionary<Area3D, ItemMarker> markedPositions = new Dictionary<Area3D, ItemMarker>();

	public Camera3D camera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		updateMarkers();
	}

	public void addMarkedPos(Area3D pos){
		ItemMarker marker;

		if(markedPositions.ContainsKey(pos)){

			markedPositions.TryGetValue(pos, out marker);

		}else{

			marker = itemMarker.Instantiate() as ItemMarker;

			AddChild(marker);
			markedPositions.Add(pos, marker);
		}

		itemMarkerLerper lerper = new itemMarkerLerper(marker, 0.6f, pos);
		marker.addLerper(lerper);
		
	}

	public void removeMarkedPos(Area3D pos){

		if(markedPositions.ContainsKey(pos)){
			ItemMarker marker;
			markedPositions.TryGetValue(pos, out marker);

			itemMarkerLerper lerper = new itemMarkerLerper(marker, 0.0f, pos);
			marker.addLerper(lerper);
			
			lerper.LerpFinished += () => {deleteMarker(marker, pos);};
		}	
	}

	public void hideMarkers(){
		
	}

	public void showMarkers(){

	}

	private void deleteMarker(ItemMarker marker, Area3D pos){
			markedPositions.Remove(pos);
			marker.QueueFree();
	}

	private void updateMarkers(){
		foreach(var item in markedPositions){
			if(IsInstanceValid(item.Key)){

				//item.Value.Visible = !camera.IsPositionBehind(item.Key.GlobalTransform.Origin);
				if(itemVisible(item.Key)){
					item.Value.Visible = true;
					Node3D parent = item.Key.GetParent() as Node3D;
					Vector2 pos = camera.UnprojectPosition(parent.GlobalTransform.Origin);
					pos = new Vector2(pos.X - (item.Value.Size.X * 0.5f), pos.Y - (item.Value.Size.Y * 0.5f) );
					item.Value.GlobalPosition = pos;
				}else{
					item.Value.Visible = false;
				}


			}else{markedPositions.Remove(item.Key);}
		}
	
	}

	private bool itemVisible(Area3D obj){

		if(camera.IsPositionBehind(obj.GlobalTransform.Origin)){
			return false;
		}

		if(!MainCamera.instance.isVisibleOnScreen(obj, 2)){
			return false;
		}



		return true;
	}

}
