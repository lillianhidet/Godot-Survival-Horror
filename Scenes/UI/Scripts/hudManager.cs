using Godot;
using System;
using System.Collections.Generic;
using System.Formats.Tar;

public partial class hudManager : Control{

	public static hudManager Instance { get; private set;}

	static RichTextLabel text;
	static Timer timer;
	static TextureRect ret;

	static Label ammoHeld;
	static Label ammoLoaded;

	public Camera3D camera;



	static ItemMarkersManager nearbyMarkerParent;
	//Dictionary<Node3D, TextureRect> markedPositions = new Dictionary<Node3D, TextureRect>();


	//[Export] Texture2D nearbyMarker;

    public override void _Ready(){
		Instance = this;
		ammoHeld = GetNode<Label>("CanvasLayer/ammo/inInv");
		ammoLoaded = GetNode<Label>("CanvasLayer/ammo/inMag");
        text = GetNode<RichTextLabel>("%HUDText");
		timer = GetNode<Timer>("%Timer");
		ret = GetNode<TextureRect>("CanvasLayer/ret");
		nearbyMarkerParent = GetNode<ItemMarkersManager>("CanvasLayer/nearbyItemMarkers");
		//lol
		
    }

   /* public override void _Process(double delta){
		updateMarkers();
        
    }*/


    public static void displayhudtext(string toAdd, int lifespan){
		text.Text = "[center]" + toAdd + "[/center]";
		timer.WaitTime = lifespan;
		timer.Start();
	}

	public void timerEnd(){
		text.Text = "";
	}

	public static void setreticulePos(Vector2 screenspace){
		ret.Visible = true;
		Vector2 sc = screenspace;
		sc = new Vector2(sc.X - (ret.Size.X * 0.5f), sc.Y - (ret.Size.Y * 0.5f) );
		ret.SetGlobalPosition(sc);
	}

	public static void hideReticule(){
		ret.Visible = false;
	}

	public static void updateInvAmmoLabel(int ammo){
		String newVal;
		if(ammo<10){
			newVal = "0" + ammo.ToString();
		}else{
			newVal = ammo.ToString();
		}
			ammoHeld.Text = newVal;
	}

	public static void updateLoadedAmmoLabel(int ammo){
		String newVal;
		if(ammo<10){
			newVal = "0" + ammo.ToString();
		}else{
			newVal = ammo.ToString();
		}
			ammoLoaded.Text = newVal;

	}

	public void addMarkedPos(Area3D pos){
		if(nearbyMarkerParent.camera == null){nearbyMarkerParent.camera = camera;}
		nearbyMarkerParent.addMarkedPos(pos);
		/*if(!markedPositions.ContainsKey(pos)){
			TextureRect rect = new TextureRect();
			rect.Texture = nearbyMarker;

			rect.Modulate = new Color(rect.Modulate.R, rect.Modulate.G, rect.Modulate.B, 0.0f);

			nearbyMarkerParent.AddChild(rect);

			itemMarkerLerper lerper = new itemMarkerLerper(rect, 0.6f, pos);
			rect.AddChild(lerper);

			markedPositions.Add(pos, rect);
		}*/
		
		
	}

	public void removeMarkedPos(Area3D pos){
		nearbyMarkerParent.removeMarkedPos(pos);
		/*if(markedPositions.ContainsKey(pos)){
			TextureRect rect;
			markedPositions.TryGetValue(pos, out rect);

			itemMarkerLerper lerper = new itemMarkerLerper(rect, 0.0f, pos);
			rect.AddChild(lerper);
			
			lerper.LerpFinished += () => {deleteMarker(rect, pos);};

		}*/
		
	}

	/*private void deleteMarker(TextureRect rect, Node3D pos){
			markedPositions.Remove(pos);
			rect.QueueFree();
	}*/

	/*private void updateMarkers(){
		foreach(var item in markedPositions){
				item.Value.Visible = !camera.IsPositionBehind(item.Key.GlobalTransform.Origin);
				Vector2 pos = camera.UnprojectPosition(item.Key.GlobalTransform.Origin);
				pos = new Vector2(pos.X - (item.Value.Size.X * 0.5f), pos.Y - (item.Value.Size.Y * 0.5f) );
				item.Value.GlobalPosition = pos;
		}
	
	}*/


}
