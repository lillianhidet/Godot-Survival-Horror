using Godot;
using System;

public partial class hudManager : Control{

	static RichTextLabel text;
	static Timer timer;
	static TextureRect ret;

    public override void _Ready(){
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
		Vector2 sc = screenspace * 0.75f;
		sc = new Vector2(sc.X - (ret.Size.X * 0.5f), sc.Y - (ret.Size.Y * 0.5f) );
		ret.SetGlobalPosition(sc);
	}

	public static void hideReticule(){
		ret.Visible = false;
	}
}
