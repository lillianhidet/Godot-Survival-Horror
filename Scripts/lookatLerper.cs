using Godot;
using System;

public partial class lookatLerper : Node{

    Node3D looker;
    Node3D target;

    Node3D helper;

    float t = 0;
    float speed;

    bool deathScheduled = false;


    public lookatLerper(Node3D looker, Node3D target, float speed, float xAxisLimit, float yAxisLimit){
		
		helper = new Node3D();
		looker.AddChild(helper);
		helper.LookAt(target.GlobalPosition);

        helper.RotationDegrees = new Vector3(
            Math.Clamp(helper.RotationDegrees.X, -xAxisLimit, xAxisLimit),
            Math.Clamp(helper.RotationDegrees.Y, -yAxisLimit, yAxisLimit), 
            helper.RotationDegrees.Z);

        this.target = target;
        this.looker = looker;
        this.speed = speed;


	}

    public override void _Process(double delta){
        looker.RotationDegrees = looker.RotationDegrees.Lerp(helper.RotationDegrees, t);
        t+=(float)delta * speed;

        if(t >= 1 || looker.RotationDegrees == helper.RotationDegrees || deathScheduled){
            helper.QueueFree();
            QueueFree();
        }


    }

    public void scheduleDeath(){
        deathScheduled = true;
    }
}