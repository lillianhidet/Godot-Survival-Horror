using Godot;
using System;

public partial class chase : State{
	[Export] NavigationAgent3D agent;
	[Export] EnemyBase body;

	CharacterBody3D target;
	private bool has_target;

	public override void enter(){
        target = (CharacterBody3D) GetTree().GetFirstNodeInGroup("player");
		agent.TargetPosition = target.GlobalPosition;
		
    }

    public override void exit(){
        
    }

    public override void update(double delta){
        
    }

    public override void physics_update(double delta){
        if(agent.IsTargetReached()){
            //Attack
            
            
        }else{
			agent.TargetPosition = target.GlobalPosition;
            Vector3 nextPos = agent.GetNextPathPosition();
            body.Velocity = body.GlobalPosition.DirectionTo(nextPos) * body.idleSpeed;
            if(!body.GlobalPosition.IsEqualApprox(nextPos)){
                body.LookAt(nextPos);
            }
            
            body.MoveAndSlide();

        }
    }
	


}
