using Godot;
using System;

//Class which managers functions called directly by animations via the function call track
public partial class AnimationEventDispatcher : Node{

	//For testing purposes only, this should be accessed via the inventory class
	Node3D leftHand;
	playerState playerState;
	Node3D Lantern;

	AnimationManager animManager;

	//Temp - testing only
	[Export] Node3D player;


	

    public override void _Ready(){
        playerState = GetNode<playerState>("/root/PlayerState");
		leftHand = GetNode<Node3D>("%LeftHandPos");
		animManager = GetNode<AnimationManager>("%AnimationManager");
    }
    void startLockedAnim(){
		playerState.lockAnim();
		
	}
	void endLockedAnim(){
		playerState.unlockAnim();
	}

//Stop the IK on the hand holding the lantern so it can be put back down
	/*void startPickupLantern(){
		startLockedAnim();
		if(playerInventory.holdingLantern){

			animManager.stopHoldLantern();
			Lantern = (Node3D) leftHand.GetChild(0);
			Lantern.Rotation = new Vector3(0,0,0);


			
		}
	}
*/
//Start the IK on the lantern so that it is held out infront again
	void stopPickupLantern(){
		endLockedAnim();
	}

	void pickupdropLantern(){
		if(playerInventory.holdingLantern){
			Lantern = (Node3D) leftHand.GetChild(0);
			Lantern.Reparent(GetTree().Root);
			//This should likely be set the the position of a downward raycast, to ensure it sits on the ground properly

			//Face the lantern in the direction the player is facing
			Lantern.Rotation = new Vector3(0, player.GlobalRotation.Y,0);
			playerInventory.holdingLantern = false;

		}else{
			//Check if lantern is close
			
			Lantern.Reparent(leftHand);
			animManager.startHoldLantern();
			Lantern.Position = new Vector3(0, 0, 0);
			Lantern.Rotation = new Vector3(0,0,0);
			
			
			playerInventory.holdingLantern = true;


		}
	}

}
