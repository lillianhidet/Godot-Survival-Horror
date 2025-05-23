using Godot;
using System;

public partial class textItem : interactable{
  //Refactor this to a general class that loads text windows
    [Export] string text;
    [Export] Texture2D image;
  [Export] Color buttonColour;

    
    public override void interact()
  {

    Node n = textWindowManager.loadScene();
    GetTree().Root.AddChild(n);

    textWindowManager.loadText(false, text);
    textWindowManager.setImage(image);
    textWindowManager.addButton(textWindowManager.close, "Exit", buttonColour);

  }
}
 