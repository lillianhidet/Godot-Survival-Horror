using Godot;
using System;

public partial class AnimationManager : Node{

	private static AnimationManager instance;
	public static AnimationTree player;

	private static SkeletonIK3D leftArmIK;
	private static SkeletonIK3D rightArmHalfIK;

	private static Node3D rightArmIKTarget;
	private static Node3D leftArmIKTarget;
	bool lerpedToAim = false;
	bool lerpedFromAim = true;


	float i = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		player = GetNode<AnimationTree>("%AnimationTree");

		if(instance==null){
			instance = this;
		}else {GD.PushError("Attempted to create second instance of singleton");}

	}

	public static AnimationManager getInstance(){
		return instance;
	}

    public override void _Process(double delta){
        if(playerState.IsAiming && !lerpedToAim){
			lerpAimUp();
		}else if(!playerState.IsAiming && !lerpedFromAim){
			lerpAimDown();
		}
    }

	public void startHoldLantern(){
		animationLerper lanternLerper;

		lanternLerper = new animationLerper(4f, player, true, "parameters/LeftArmBlend/blend_amount");
		this.AddChild(lanternLerper);
	}

    void lerpAimUp(){

		animationLerper Rlerper;
	

		/*if(playerInventory.holdingLantern){
			lanternLerper = new animationLerper(4f, player, true, "parameters/LeftArmBlend/blend_amount");
				this.AddChild(lanternLerper);
		}*/

		if(playerInventory.holdingOneHanded){
			
			if(!playerInventory.holdingLantern){
				Rlerper = new animationLerper(4f, player, true,"parameters/twoHandPistolBlend/blend_amount");
			}else{
				Rlerper = new animationLerper(4f, player, true,"parameters/oneHandPistolBlend/blend_amount");

				
			}

			this.AddChild(Rlerper);
			
		}

		lerpedToAim = true;
		lerpedFromAim = false;
	}

	void lerpAimDown(){
	
		

		animationLerper Rlerper;
		

		/*if(playerInventory.holdingLantern){
			lanternLerper = new animationLerper(4f, player, false, "parameters/LeftArmBlend/blend_amount");
			this.AddChild(lanternLerper);
		}*/

		if(playerInventory.holdingOneHanded){

			if(!playerInventory.holdingLantern){
				Rlerper = new animationLerper(4f, player, false, "parameters/twoHandPistolBlend/blend_amount");
			}else{
				Rlerper = new animationLerper(4f, player, false, "parameters/oneHandPistolBlend/blend_amount");
			}

			this.AddChild(Rlerper);
			
		}

		
		
		lerpedToAim = false;
		lerpedFromAim = true;

	}

	public void createLerper(float speed, bool up, string paramPath = "",SkeletonIK3D ik = null ){
		animationLerper lerper = new animationLerper(speed, player, up,paramPath, ik);
		this.AddChild(lerper);

	}

	/*public void startHoldLantern(){
		animationLerper lerper = new animationLerper(1.2f, player, true,"parameters/LeftArmBlend/blend_amount");
		this.AddChild(lerper);
	}*/

	/*public void collectLanternFirstTime(){
		leftArmIK.Start();
		player.Set("parameters/LeftArmBlend/blend_amount", 1);
	}*/



	//public void stopHoldLantern(){
		
		
	//}

	

	public void takeHolsterLantern(){

		if(playerInventory.holdingLantern){
			EquipManager.startMovingToAttach();
			animationLerper lerper = new animationLerper(1.2f, player, false,"parameters/LeftArmBlend/blend_amount");
			this.AddChild(lerper);

			animationLerper lerper2 = new animationLerper(1.2f, player, true,"parameters/LanternHolsterBlend/blend_amount");
			this.AddChild(lerper2);
			lerper2.LerpFinished += EquipManager.attachLanternToBody;

		}else{

			animationLerper lerper = new animationLerper(1.2f, player, true,"parameters/LanternHolsterBlend/blend_amount");
			this.AddChild(lerper);
			lerper.LerpFinished += EquipManager.attachLanternToHand;


		}

	}

	public void returnHand(){

		if(!playerInventory.holdingLantern){

			animationLerper lerper = new animationLerper(1.2f, player, false,"parameters/LanternHolsterBlend/blend_amount");
			this.AddChild(lerper);
			
		}else{

			animationLerper lerper = new animationLerper(1.2f, player, false,"parameters/LanternHolsterBlend/blend_amount");
			this.AddChild(lerper);

			lerper.LerpFinished += EquipManager.doneMovingAway;

			animationLerper lerper2 = new animationLerper(1.2f, player, true,"parameters/LeftArmBlend/blend_amount");
			this.AddChild(lerper2);

		}

	}

	public void walk(float speed){

		player.Set("parameters/Movement/blend_position", speed);

	}

	public static void firePistol2H(){
		player.Set("parameters/firePistol2HOneshot/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public static void firePistol1H(){
		player.Set("parameters/firePistol1HOneshot/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public void startPistolReload(){
		animationLerper lerper = new animationLerper(4f, player, true,"parameters/pistolReloadStart/blend_amount");
		this.AddChild(lerper);
		lerper.LerpFinished += () => player.Set("parameters/pistolReload/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public void endPistolReload(){
		animationLerper lerper = new animationLerper(4f, player, false,"parameters/pistolReloadStart/blend_amount");
		this.AddChild(lerper);
	}

	
}
