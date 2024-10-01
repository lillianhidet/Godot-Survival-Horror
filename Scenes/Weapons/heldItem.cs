using Godot;
using System;

public partial class heldItem : Node3D{
	//Think I should honestly just have a class for ranged weapon, and class for melee weapon, which inherrit from this
	public enum equipStyle{
		twoHanded,
		oneHanded,
		flex
	};

	public enum type{
		ranged,
		melee,
		util,
		lantern
	};

	[Export] public equipStyle itemStyle;
	[Export] public type itemType;
	[Export] Node3D leftHandLocation;
	[Export] Node3D rightHandLocation;

	public bool held;
	


     public void use(){}   

}


