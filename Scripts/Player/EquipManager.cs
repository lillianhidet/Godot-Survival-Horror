using Godot;
using System;

//Handles adding equippable items to the slots on the player 
public partial class EquipManager : Node3D{

	static Node3D leftHandSlot;
	static Node3D rightHandSlot;
	static AnimationManager animManager;
	public override void _Ready(){

		leftHandSlot = GetNode<Node3D>("%LeftHandPos");
		rightHandSlot = GetNode<Node3D>("%RightHandPos");
		animManager = GetNode<AnimationManager>("%AnimationManager");
	}

	public static void equipLantern(heldItem lantern){
		//Node3D i = (Node3D)lantern.equipable.Instantiate();
		//leftHandSlot.AddChild(i);
		lantern.Reparent(leftHandSlot, false);
		lantern.Visible = true;
		animManager.collectLanternFirstTime();
		playerInventory.holdingLantern = true;
	}
	public static void equipItemRight(heldItem item){
		//Node3D i = (Node3D)item.equipable.Instantiate();
		item.Reparent(rightHandSlot, false);
		item.Visible = true;
		//rightHandSlot.AddChild(item);
		playerInventory.holdingOneHanded = true;
		playerInventory.holdingTwoHanded = false;

	}

	public static void equipitemBoth(heldItem item){
		item.Reparent(rightHandSlot, false);
		item.Visible = true;
		playerInventory.holdingOneHanded = false;
		playerInventory.holdingTwoHanded = true;
	}

}
