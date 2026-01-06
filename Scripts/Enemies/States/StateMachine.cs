using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node{

	[Export] public State initial;
	[Export] public Label3D debug;
	private State currentState;

	private Dictionary<string, State> states = new Dictionary<string, State>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		if(initial !=null){
			currentState = initial;
			debug.Text = initial.Name;
		}else{
			GD.PushWarning("No initial state");
		}

		foreach (Node n in GetChildren()) {
			if (n is State) {
				State s = (State)n;
				states.Add(n.Name.ToString().ToLower(), s);
				s.setMachine(this);
			}
		}

		currentState.enter();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
        if (currentState != null) {
            currentState.update(delta);
        }
    }

    public override void _PhysicsProcess(double delta) {
        if(currentState != null){
			currentState.physics_update(delta);
		}
    }

	public void transitionState(State from, string to){
		if(from != currentState){
			GD.PushWarning("Non-active state tried to transition out");
			return;
		}

		State newState = states[to.ToLower()];

		if(newState == null){
            GD.PushWarning("Tried to switch to non-existent state");
			return;
        }

		if(currentState!= null){
			currentState.exit();
		}

		newState.enter();
		currentState = newState;
		debug.Text = currentState.Name;

	}


}
