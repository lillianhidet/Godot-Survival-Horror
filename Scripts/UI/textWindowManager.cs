using Godot;
using Godot.Collections;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class textWindowManager : Node
{
	static PackedScene textDisplayScene;
	static PackedScene objectDisplayScene;

	static PackedScene button;
	//static bool textLoaded;
	static Node loaded;
	static RichTextLabel imgTextLbl;
	static RichTextLabel objTextLbl;
	static TextureRect imgLbl;

	static Array<String> textA;
	static Button LButton;
	static Button RButton;
	static int currentPage;

	//to be stored in global vars
	static float textSpeed = 10f;


    public override void _Ready(){

        textDisplayScene = GD.Load<PackedScene>("uid://biqxqmaaftq2x");
		objectDisplayScene = GD.Load<PackedScene>("uid://c4e4cxpkf8wxs");

		button = GD.Load<PackedScene>("uid://pkloy387l3nc");
		//textLoaded = false;
    }

	public static Node loadTextScene(Array<String> text, Texture2D img){
		loaded = textDisplayScene.Instantiate();
		imgTextLbl = (RichTextLabel)loaded.GetNode("%ImageText");
		imgLbl = (TextureRect)loaded.GetNode("%TextImage");
		LButton = (Button)loaded.GetNode("%LButton");
		RButton = (Button)loaded.GetNode("%RButton");

		LButton.Pressed += textLeft;
		RButton.Pressed += textRight;
		textA = text;

		LButton.Visible = false;
		if(textA.Count == 1){ RButton.Visible = false; }
    
        imgLbl.Texture = img;
		currentPage = 0;
        Helpers.tweenText(textA[currentPage], imgTextLbl, textSpeed);

        playerState.openMenu();
		//Do we need to return?
		return loaded;
	}

	public static void textLeft(){
		if (currentPage != 0){
            RButton.Visible = true;
            currentPage--;
            Helpers.tweenText(textA[currentPage], imgTextLbl, textSpeed);
        } 
		
		if (currentPage == 0) {
            LButton.Visible = false;
        }

	}

	public static void textRight(){
		if (currentPage < textA.Count - 1){
			
            LButton.Visible = true;
            currentPage++;
            Helpers.tweenText(textA[currentPage], imgTextLbl, textSpeed);
		}

		if (currentPage == textA.Count - 1) { 
			RButton.Visible = false;
		}
    }

	public static Node loadObjScene(Viewport v, String t){

		loaded = objectDisplayScene.Instantiate();
		objTextLbl = (RichTextLabel)loaded.GetNode("%ObjectText");
		imgLbl = (TextureRect)loaded.GetNode("%TextImage");

        imgLbl.Texture = v.GetTexture();

        Helpers.tweenText(t, objTextLbl,textSpeed);

        playerState.openMenu();

		return loaded;

	}


	// Todo - Make accessible from anywhere
	/*static void tweenText(string text, RichTextLabel label){
        float duration = text.Length / textSpeed;
		label.Text = text;
		label.VisibleCharacters = 0;

		Tween t = label.CreateTween();

		t.TweenProperty(label, "visible_characters", text.Length, duration);

    }*/
	

	
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
