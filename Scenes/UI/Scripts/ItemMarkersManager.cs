using Godot;
using System;
using System.Collections.Generic;

public partial class ItemMarkersManager : Control{

	[Export] PackedScene itemMarker;

	Dictionary<Area3D, ItemMarker> markedPositions = new Dictionary<Area3D, ItemMarker>();

	public Camera3D camera;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		updateMarkers();
	}

	public void addMarkedPos(Area3D pos){
		ItemMarker marker;

		if(markedPositions.ContainsKey(pos)){

			markedPositions.TryGetValue(pos, out marker);
			marker.deleting = false;

		}else{

			marker = itemMarker.Instantiate() as ItemMarker;

			AddChild(marker);
			markedPositions.Add(pos, marker);
		}

		fadeIn(marker, pos);
		
	}

	public void removeMarkedPos(Area3D pos){

		if(markedPositions.ContainsKey(pos)){
			
			ItemMarker marker = markedPositions[pos];
			marker.deleting = true;

			itemMarkerLerper lerper = fadeOut(marker, pos);
			
			lerper.LerpFinished += () => {deleteMarker(marker, pos);};
		}	
	}


	private void deleteMarker(ItemMarker marker, Area3D pos){

			markedPositions.Remove(pos);
			marker.QueueFree();
	}

	private void updateMarkers(){
		foreach(var item in markedPositions){
			if(IsInstanceValid(item.Key)){

				//item.Value.Visible = !camera.IsPositionBehind(item.Key.GlobalTransform.Origin);
				
				if(itemVisible(item.Key) && item.Value.deleting == false){
					
					if(item.Value.fadingIn == false){
						fadeIn(item.Value,item.Key);
					}
					
				}else{

					if(item.Value.fadingOut == false){
						fadeOut(item.Value,item.Key);
					}
					
					
				}

				Node3D parent = item.Key.GetParent() as Node3D;
				Vector2 pos = camera.UnprojectPosition(parent.GlobalTransform.Origin);
				pos = new Vector2(pos.X - (item.Value.Size.X * 0.5f), pos.Y - (item.Value.Size.Y * 0.5f) );
				item.Value.GlobalPosition = pos;


			}else{markedPositions.Remove(item.Key);}
		}
	
	}

	private bool itemVisible(Area3D obj){
			
			
		if(camera.IsPositionBehind(obj.GlobalTransform.Origin)){
			return false;
		}

		if(MainCamera.instance.isVisibleOnScreen(obj, 2)){
			return true;
		}else{
			return false;
		}

	}

	public void fadeIn(ItemMarker marker, Area3D pos){
		marker.fadingIn = true;
		marker.fadingOut = false;
		itemMarkerLerper lerper = new itemMarkerLerper(marker, 0.6f, pos);
		marker.addLerper(lerper);

	}

	public itemMarkerLerper fadeOut(ItemMarker marker, Area3D pos){
		marker.fadingIn = false;
		marker.fadingOut = true;
		itemMarkerLerper lerper = new itemMarkerLerper(marker, 0.0f, pos);
		marker.addLerper(lerper);

		return lerper;
	}

}
