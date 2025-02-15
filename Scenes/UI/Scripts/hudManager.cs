using Godot;
using System;

public partial class hudManager : Control{

	static RichTextLabel text;
	static Timer timer;
	static TextureRect ret;

	static Label ammoHeld;
	static Label ammoLoaded;

    public override void _Ready(){
		ammoHeld = GetNode<Label>("ammo/inInv");
		ammoLoaded = GetNode<Label>("ammo/inMag");
        text = GetNode<RichTextLabel>("%HUDText");
		timer = GetNode<Timer>("%Timer");
		ret = GetNode<TextureRect>("ret");
    }
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
}
