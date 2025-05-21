using Godot;
using System;

public partial class textWindowManager : Node
{
	static PackedScene textDisplayScene;
	static PackedScene objectDisplayScene;

	static PackedScene button;
	public static bool textLoaded;
	static Node loaded;
	static RichTextLabel imgTextLbl;
	static RichTextLabel objTextLbl;
	static TextureRect imgLbl;

    public override void _Ready(){

        textDisplayScene = GD.Load<PackedScene>("uid://biqxqmaaftq2x");
		objectDisplayScene = GD.Load<PackedScene>("uid://c4e4cxpkf8wxs");

		button = GD.Load<PackedScene>("uid://pkloy387l3nc");
		textLoaded = false;
    }

	public static Node loadScene(){
		loaded = textDisplayScene.Instantiate();
		imgTextLbl = (RichTextLabel)loaded.GetNode("%ImageText");
		objTextLbl = (RichTextLabel)loaded.GetNode("%ObjectText");
		imgLbl = (TextureRect)loaded.GetNode("%TextImage");

		//Button b = (Button) loaded.GetNode("%ExitBtn");
		//b.Pressed += close;

		playerState.openMenu();
		//Do we need to return?
		return loaded;
	}

	public static Node loadObjScene(){

		loaded = objectDisplayScene.Instantiate();
		objTextLbl = (RichTextLabel)loaded.GetNode("%ObjectText");
		imgLbl = (TextureRect)loaded.GetNode("%TextImage");

		playerState.openMenu();

		return loaded;

	}

	//Simplify
	public static void loadText(bool obj, String text){
		if (loaded != null){

			if (text != ""){
				if (!obj){
					imgTextLbl.Text = text;
					objTextLbl.Visible = false;
				}
				else{
					objTextLbl.Text = "[center]" + text + "[/center]";
					imgTextLbl.Visible = false;


				}

			}else { GD.PushWarning("Unnassigned Text"); }


		}else { GD.PushWarning("Load text scene before assigning text"); }
		
	}

	public static void loadObj(String text){

		if (loaded != null){

			if (text != ""){

				objTextLbl.Text = text;
				
			}
			else { GD.PushWarning("Unnassigned Text"); }


		}
		else { GD.PushWarning("Load text scene before assigning text"); }
		
		
	}
	

	public static void setImage(Texture2D image)
	{
		if (loaded != null)
		{

			if (image != null)
			{

				imgLbl.Texture = image;

			}
			else { GD.PushWarning("Unassigned Image"); }


		}
		else { GD.PushWarning("Load text scene before assigning an image"); }

	}

	public static void setViewportTexture(Viewport v){
	
	
       // ViewportTexture t = new ViewportTexture{ViewportPath = v.GetPath()};

		imgLbl.Texture = v.GetTexture();

	}
	
	public static void addButton(Action action, string text, Color col){

		Button b = (Button)button.Instantiate();
		b.Set("theme_override_colors/font_color", col);
		b.Pressed += action;
		b.Text = text;
		loaded.GetNode("%ButtonCont").AddChild(b);


	}

	public static void close(){
		playerState.closeMenu();
		loaded.QueueFree();
		

	}


}
