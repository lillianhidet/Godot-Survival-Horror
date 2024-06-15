using Godot;
using System;

public partial class AnimationManager : Node
{
	static AnimationTree player;
	private static SkeletonIK3D leftArmIK;
	private static SkeletonIK3D rightArmHalfIK;

	private static Node3D rightArmIKTarget;
	private static Node3D leftArmIKTarget;
	bool lerpedToAim = false;
	bool lerpedFromAim = true;

	//bool lerpUp = false;
	//float increment = 1.2f;
	//bool lerping = false;
	//string paramPath = "";
	//SkeletonIK3D ik;	

	float i = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		player = GetNode<AnimationTree>("%AnimationTree");
		leftArmIK = GetNode<SkeletonIK3D>("%LeftArmIK");
		rightArmHalfIK = GetNode<SkeletonIK3D>("%RightArmHalfIK");
		leftArmIKTarget = GetNode<Node3D>("%LeftArmTarget");
		rightArmIKTarget = GetNode<Node3D>("%RightArmTarget");

	}

    public override void _Process(double delta){
        if(playerState.IsAiming && !lerpedToAim){
			lerpAimUp();
		}else if(!playerState.IsAiming && !lerpedFromAim){
			lerpAimDown();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    //Will likely at some point want to have the lerp based on animation length
    /*public override void _Process(double delta){

		//Extremely messy
		if(lerping){
				float l;

			if(lerpUp){
				l = 0 + (1 - 0) * i;
			}else{
				l = 1 + (0 - 1) * i;
			}

			i +=(float)delta*increment;

			if(paramPath!=""){
				player.Set(paramPath, l);
			}if(ik != null){
				ik.Interpolation = l;
				//if(ik.Interpolation <= 0){ik.Stop();}
			}
			//Really bad
			if((lerpUp && l >= 1) || (!lerpUp && l <= 0)){
				lerping = false;
				paramPath = "";
				ik = null;
				i = 0;
			}
		}
	}*/

    /*public void startLerp(bool lerpUp, string paramPath = "", SkeletonIK3D ik = null){

		if(paramPath == "" && ik == null){
			GD.PushWarning("Tried to start an animation lerp without a IK or Parampath.");
		}else{
			this.lerpUp = lerpUp;
			this.paramPath = paramPath;
			this.ik = ik;
			i=0;
			lerping = true;
			
		}

	}*/

    void lerpAimUp(){
		
		//if(playerInventory.holdingLantern){
			//animationLerper Llerper = new animationLerper(player, true, null, leftArmIK);
			//this.AddChild(Llerper);
		//}
		if(playerInventory.holdingOneHanded){

			animationLerper Rlerper;
			
			if(!playerInventory.holdingLantern){
				Rlerper = new animationLerper(4f, player, true,"parameters/twoHandPistolBlend/blend_amount");
			}else{
				Rlerper = new animationLerper(4f, player, true, null, rightArmHalfIK);
			}

			this.AddChild(Rlerper);
			lerpedToAim = true;
			lerpedFromAim = false;
		}
	}

	void lerpAimDown(){

		animationLerper Rlerper;

		if(!playerInventory.holdingLantern){
			Rlerper = new animationLerper(4f, player, false, "parameters/twoHandPistolBlend/blend_amount");
		}else{
			Rlerper = new animationLerper(4f, player, false, null, rightArmHalfIK);
		}

		this.AddChild(Rlerper);
		lerpedFromAim = true;
		lerpedToAim = false;
		

	}

	public void createLerper(float speed, bool up, string paramPath = "",SkeletonIK3D ik = null ){
		animationLerper lerper = new animationLerper(speed, player, up,paramPath, ik);
		this.AddChild(lerper);

	}

	public void startHoldLantern(){
		//leftArmIK.Start();
		//leftArmIK.Interpolation = 0;
		//startLerp(true,"parameters/LeftArmBlend/blend_amount", leftArmIK);
		animationLerper lerper = new animationLerper(1.2f, player, true,"parameters/LeftArmBlend/blend_amount");
		this.AddChild(lerper);
	}

	public void collectLanternFirstTime(){
		//leftArmIK.Start();
		player.Set("parameters/LeftArmBlend/blend_amount", 1);
	}

	public void stopHoldLantern(){
		
		animationLerper lerper = new animationLerper(1.2f, player, false,"parameters/LeftArmBlend/blend_amount");
		this.AddChild(lerper);
	}

	

	public void pickupDropLantern(){
			player.Set("parameters/CrouchDown/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public void walk(float speed){

		player.Set("parameters/Movement/blend_position", speed);

	}

	
}
