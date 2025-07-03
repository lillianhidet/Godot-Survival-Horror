using Godot;
using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Json = Godot.Json;


public partial class DialogueHandler : interactable{
	// Called when the node enters the scene tree for the first time.
	[Export] Json storyJson;
	
	Story story;
	DialogueUI ui;

	RichTextLabel text;
	RichTextLabel name;
	Control buttonParent;
	CanvasLayer canvas;
	//make a global for settings
    static float textSpeed = 10f;

	Callable c;

    public override void _Ready(){
		//string s = ((string)storyJson.Data);
        //GD.Print(s);
		story = new Story(storyJson);
		ui = DialogueUI.Instance;

		text = ui.GetNode<RichTextLabel>("%MainDialogue");
		name = ui.GetNode<RichTextLabel>("%NameField");
		buttonParent = ui.GetNode<Control>("%ButtonHolder");
		canvas = ui.GetNode<CanvasLayer>("%Canvas");

		c = new Callable(this, "addChoices");
		
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void interact() {
        //make this a fade in
        canvas.Visible = true;
        advanceDialoge();
		playerState.openMenu();
	
    }

	public void advanceDialoge(){ 

		if(story.canContinue){
            string next = story.Continue();
			Helpers.tweenText(next, text, textSpeed, c);
            

            if (story.currentTags.Count > 0){ 
				name.Text = story.currentTags[0];
			}
			

			if(story.currentChoices.Count > 0){ 
				

					//addChoices();
				

			}
		}
	}

	public void makeChoice(int index){ 
		story.ChooseChoiceIndex(index);
		clearChoices();
		advanceDialoge();

	}

	public void addChoices(){
		foreach(Choice c in story.currentChoices){
			int index = c.index;
			string text = c.text;

			Button b = (Button)ui.choiceButton.Instantiate();
			b.Text = (index + ". " + text);
			b.Pressed += () => makeChoice(index);
			buttonParent.AddChild(b);
		}
	}

	public void clearChoices(){ 
		foreach(Node n in buttonParent.GetChildren()){ 
			n.QueueFree();
		}
	}


}
