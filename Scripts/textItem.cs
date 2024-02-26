using Godot;
using System;

public partial class textItem : interactable{

    [Export] string text;
    [Export] Texture2D image;

    PackedScene scene;
    Node loaded;
    playerState playerState;
    public override void _Ready(){
      scene = GD.Load<PackedScene>("uid://biqxqmaaftq2x");
      playerState = GetNode<playerState>("/root/PlayerState");
    }
    public override void interact(){

      loaded = scene.Instantiate();

      //Set text
      RichTextLabel textLbl = (RichTextLabel)loaded.GetNode("%Text");

      if(text!=null && text != ""){
        textLbl.Text = "[center]"+text+"[/center]";
      }else{GD.PushWarning("Unnassigned Text");}


      //Set image
      TextureRect imgLbl = (TextureRect)loaded.GetNode("%TextImage");

      if(image != null){
      imgLbl.Texture = image;
      }else{GD.PushWarning("Unassigned Image");}

      //Assign action to button
      Button b = (Button)loaded.GetNode("%ExitBtn");
      b.Pressed += close;


      playerState.openMenu();
      
      GetTree().Root.AddChild(loaded);
        
    }

    void close(){
      playerState.closeMenu();
      loaded.QueueFree();
    }


}
