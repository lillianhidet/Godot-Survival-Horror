using Godot;
using System;

public partial class hudManager : Control{

	static RichTextLabel text;
	static Timer timer;

    public override void _Ready(){
        text = GetNode<RichTextLabel>("%HUDText");
		timer = GetNode<Timer>("%Timer");
    }
    public static void displayhudtext(string toAdd, int lifespan){
		text.Text = "[center]" + toAdd + "[/center]";
		timer.WaitTime = lifespan;
		timer.Start();
	}

	public void timerEnd(){
		text.Text = "";
	}
}
