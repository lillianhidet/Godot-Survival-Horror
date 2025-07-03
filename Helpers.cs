using Godot;
using System;

public partial class Helpers : Node{

	public static Helpers instance;

    public override void _Ready() {
        instance = this;
    }

    public static void tweenText(string text, RichTextLabel label, float textSpeed, Callable c = new Callable()) {
        float duration = text.Length / textSpeed;
        label.Text = text;
        label.VisibleCharacters = 0;

        Tween t = label.CreateTween();

        t.TweenProperty(label, "visible_characters", text.Length, duration);
        
        if(IsInstanceValid(c.Target)){
            t.Finished += () => c.Call();
        }

    }

}
