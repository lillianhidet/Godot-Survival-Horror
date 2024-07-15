using Godot;
using System;

public partial class animationLerper : Node{
	bool lerpUp = false;
	float increment = 1.2f;
	bool lerping = false;
	string paramPath = "";
	SkeletonIK3D ik;	
	AnimationTree player;

	float i = 0;

	[Signal] public delegate void LerpFinishedEventHandler();

    public animationLerper(float increment, AnimationTree player, bool lerpUp, string paramPath = "", SkeletonIK3D ik = null){

		if(paramPath == "" && ik == null){
			GD.PushWarning("Tried to start an animation lerp without a IK or Parampath.");
		}else{
			this.lerpUp = lerpUp;
			this.paramPath = paramPath;
			this.ik = ik;
			i=0;
			this.increment = increment;
			this.player = player;
			lerping = true;
			
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
			//Extremely messy
		if(lerping){
			float l;

			i +=(float)delta*increment;

			if(lerpUp){
				l = 0 + (1 - 0) * i;
			}else{
				l = 1 + (0 - 1) * i;
			}

			l = Math.Clamp(l, 0, 1);
			

			if(paramPath!=""){
				player.Set(paramPath, l);
			}if(ik != null){
				ik.Interpolation = l;
			}
			
			//Really bad
			if((lerpUp && l >= 1) || (!lerpUp && l <= 0)){
				lerping = false;
				paramPath = "";
				ik = null;
				i = 0;
				EmitSignal(SignalName.LerpFinished);
				this.QueueFree();
			}
		}
		
	}
}
