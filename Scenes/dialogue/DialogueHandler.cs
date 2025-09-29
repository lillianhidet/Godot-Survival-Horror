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
	Control holder;

	//make a global for settings
    static float textSpeed = 12f;

	Callable c;

    public override void _Ready(){
		//string s = ((string)storyJson.Data);
        //GD.Print(s);
		story = new Story(storyJson);
		ui = DialogueUI.Instance;
		ui.current = this;

		text = ui.GetNode<RichTextLabel>("%MainDialogue");
		name = ui.GetNode<RichTextLabel>("%NameField");
		buttonParent = ui.GetNode<Control>("%ButtonHolder");
		holder = ui.GetNode<Control>("%Holder");

		

		c = new Callable(this, "addChoices");
		
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void close(){
		playerState.closeMenu();
	
	}

    public override void interact() {
        //make this a fade in
        ui.Visible = true;

        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(holder, "modulate:a", 1, 0.5);

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
			b.Text = ((index+1) + ". " + text);
			b.Pressed += () => makeChoice(index);
			buttonParent.AddChild(b);
			Tween tween = GetTree().CreateTween();
			tween.TweenProperty(b,"modulate:a", 1, 0.5);
			
		}
	}

	public void clearChoices(){ 
		foreach(Node n in buttonParent.GetChildren()){ 
			n.QueueFree();
		}
	}


}
