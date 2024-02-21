using Godot;
using System;

public partial class textItem : interactable{

    [Export] String text;
    [Export] Texture2D image;

    PackedScene scene;
    public override void _Ready(){

      scene = GD.Load<PackedScene>("uid://biqxqmaaftq2x");

    }
    public override void interact(){

      var loaded = scene.Instantiate();

      RichTextLabel textLbl = (RichTextLabel)loaded.GetNode("%Text");

      if(text!=null && text != ""){
        textLbl.Text = "[center]"+text+"[/center]";
      }else{GD.PushWarning("Unnassigned Text");}

      TextureRect imgLbl = (TextureRect)loaded.GetNode("%TextImage");

      if(image != null){
      imgLbl.Texture = image;
      }else{GD.PushWarning("Unassigned Image");}
      


      GetTree().Root.AddChild(loaded);
        
    }


}
