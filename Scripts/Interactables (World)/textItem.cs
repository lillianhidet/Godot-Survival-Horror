using Godot;
using System;

public partial class textItem : interactable{
  //Refactor this to a general class that loads text windows
    [Export] string text;
    [Export] Texture2D image;

    
    public override void interact(){

      Node n = textWindowManager.loadScene();
      GetTree().Root.AddChild(n);

      textWindowManager.loadText(text);
      textWindowManager.setImage(image);

    }
}
 