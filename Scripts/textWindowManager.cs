using Godot;
using System;

public partial class textWindowManager : Node
{
	static PackedScene textDisplayScene;

	static PackedScene button;
	public static bool textLoaded;
	static Node loaded;
	static RichTextLabel textLbl;
	static TextureRect imgLbl;

    public override void _Ready(){
        textDisplayScene = GD.Load<PackedScene>("uid://biqxqmaaftq2x");
		button = GD.Load<PackedScene>("uid://pkloy387l3nc");
		textLoaded = false;
    }

	public static Node loadScene(){

		loaded = textDisplayScene.Instantiate();
		textLbl = (RichTextLabel)loaded.GetNode("%Text");
		imgLbl = (TextureRect)loaded.GetNode("%TextImage");

		//Button b = (Button) loaded.GetNode("%ExitBtn");
		//b.Pressed += close;

		playerState.openMenu();

		return loaded;

	}

	public static void loadText(String text){
		if(loaded!=null){

			if(text!=""){

				textLbl.Text = "[center]"+text+"[/center]";
			
      		}else{GD.PushWarning("Unnassigned Text");}


		}else{GD.PushWarning("Load text scene before assigning text");}
	}

	public static void setImage(Texture2D image){
		if(loaded!=null){

			if(image != null){

      			imgLbl.Texture = image;

      		}else{GD.PushWarning("Unassigned Image");}


		}else{GD.PushWarning("Load text scene before assigning an image");}

	}

	public static void setViewportTexture(Viewport v){
		
		
        ViewportTexture t = new ViewportTexture{ViewportPath = v.GetPath()};

        imgLbl.Texture = v.GetTexture();

	}
	public static void addButton(Action action, string text){
		Button b = (Button) button.Instantiate();
		b.Pressed += action;
		b.Text = text;
		loaded.GetNode("%ButtonCont").AddChild(b);

	}

	public static void close(){
		playerState.closeMenu();
		loaded.QueueFree();
		

	}


}