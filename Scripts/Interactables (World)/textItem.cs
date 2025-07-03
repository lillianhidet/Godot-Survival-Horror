using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class textItem : interactable{
    //Refactor this to a general class that loads text windows
    [Export(PropertyHint.MultilineText)] Array<String> text;
    [Export] Texture2D image;
    [Export] Color buttonColour;

    
    public override void interact(){

        Node n = textWindowManager.loadTextScene(text, image);
        GetTree().Root.AddChild(n);

        textWindowManager.addButton(textWindowManager.close, "Back", buttonColour);

  }
}
 