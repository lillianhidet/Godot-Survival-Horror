using Godot;
using System;

public partial class idle : State{

    private Vector3 target;
    [Export] NavigationAgent3D agent;
    [Export] EnemyBase body;
    RandomNumberGenerator r = new RandomNumberGenerator();
    private bool firstAttempt = true;
    

    Timer pause;

    private bool has_target;
    private bool can_move;


    public override void enter() {
        
        has_target = false;
        can_move = true;
        newTarget();
    }

    public override void exit() {

    }

    public override void update(double delta) {

    }

    public override void physics_update(double delta) {
        if(agent.IsTargetReached()){
            has_target = false;
            newTarget();

            can_move = false;
            startTimer();
            
        }

        if (has_target && can_move) {
            Vector3 nextPos = agent.GetNextPathPosition();
            body.Velocity = body.GlobalPosition.DirectionTo(nextPos) * body.idleSpeed;
            if(!body.GlobalPosition.IsEqualApprox(nextPos)){
                body.LookAt(nextPos);
            }
            
            body.MoveAndSlide();

        }


    }

    public void sightEntered(Node3D body){

        if(body.IsInGroup("player")){
            StateMachine p = (StateMachine)GetParent();
            p.transitionState(this, "chase");
        }
    }

    public void newTarget(){
        Vector3 current = body.GlobalPosition;

        target = new Vector3(current.X + r.RandiRange(-15, 15), 0, current.Z + r.RandiRange(-15, 15));
        agent.TargetPosition = target;



        //Hacky solution to first pathfinding attemp taking 1000s of attempts, but all after working fine. Likely due to loading order.
        if (firstAttempt) {
            agent.TargetPosition = current;
            has_target = true;
            firstAttempt = false;
        }else{

            if (!agent.IsTargetReachable()) {
                newTarget();
            } else {
                has_target = true;
            }

        }

            

    }

    public void startTimer(){
        pause = new Timer();
        pause.WaitTime = r.RandiRange(1, 4);
        pause.OneShot = true;
        AddChild(pause);
        pause.Timeout += () => allowMove();
        pause.Start();

    }

    public void allowMove(){ 
        pause.QueueFree();
        can_move=true;
    
    }
}
