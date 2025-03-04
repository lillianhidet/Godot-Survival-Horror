using Godot;
using System;
using System.Runtime.CompilerServices;

//Handles adding equippable items to the slots on the player 
public partial class EquipManager : Node3D{

	static Node3D leftHandSlot;
	static Node3D rightHandSlot;

	static Node3D lanternMount;

	static AnimationManager animManager;

	static Node3D lanternParent;

	static RigidBody3D handle;
	static LanternTest light;
	static RemoteTransform3D handRemote;
	static RemoteTransform3D mountRemote;
	
	public override void _Ready(){
		leftHandSlot = GetNode<Node3D>("%LanternHold");
		rightHandSlot = GetNode<Node3D>("%RightHandPos");
		lanternMount = GetNode<Node3D>("%LanternMount");
		animManager = AnimationManager.getInstance();
		//GetNode<AnimationManager>("%AnimationManager");

		handle = GetNode<RigidBody3D>("%Handle");
		light = GetNode<LanternTest>("%Light");

		handRemote = GetNode<RemoteTransform3D>("%handRemote");
		mountRemote = GetNode<RemoteTransform3D>("%mountRemote");
		

	}
	

	public static void equipLantern(heldItem lantern){
		handRemote.RemotePath = handle.GetPath();
		mountRemote.RemotePath = null;
		animManager.startHoldLantern();
		
		handle.Visible = true;
		light.Visible = true;
	
		playerInventory.holdingLantern = true;
	}

	public static void startMovingToAttach(){
		light.Freeze = true;
		light.attached = true;
		handle.Freeze = true;
	}

	public static void attachLanternToBody(){
		
		handRemote.RemotePath = null;
		mountRemote.RemotePath = handle.GetPath();
		//handle.target = lanternMount;
		
		//handle.Rotation = new Vector3(0,0,0);
		playerInventory.holdingLantern = false;

		animManager.returnHand();
		

	}

	public static void attachLanternToHand(){
		handRemote.RemotePath = handle.GetPath();
		mountRemote.RemotePath = null;
		//handle.target = leftHandSlot;
		playerInventory.holdingLantern = true;
		animManager.returnHand();
		
	
	}

	public static void doneMovingAway(){
		
	
		handle.Freeze = false;
		light.Freeze = false;
		light.attached = false;
	
		//light.moving = false;
		

	}



	public static void equipItemRight(heldItem item){
		//Node3D i = (Node3D)item.equipable.Instantiate();
		item.Reparent(rightHandSlot, false);
		//item.Visible = true;
		//rightHandSlot.AddChild(item);
		playerInventory.holdingOneHanded = true;
		playerInventory.holdingTwoHanded = false;

	}

	public static void equipitemBoth(heldItem item){
		item.Reparent(rightHandSlot, false);
		//item.Visible = true;
		playerInventory.holdingOneHanded = false;
		playerInventory.holdingTwoHanded = true;
	}

}
