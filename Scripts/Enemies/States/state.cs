using Godot;
using System;

public abstract partial class State : Node{

	private StateMachine machine;

	public void setMachine(StateMachine stateMachine){ 
		machine = stateMachine;
	}

	public abstract void enter();
	public abstract void exit();

	public abstract void update(double delta);

	public abstract void physics_update(double delta);
}
