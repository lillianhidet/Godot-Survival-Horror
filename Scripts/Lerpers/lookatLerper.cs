using Godot;
using System;

public partial class lookatLerper : Node{

    Node3D looker;
    Vector3 target;

    Node3D helper;

    Vector3 start;

    float t = 0;
    float speed;

    bool deathScheduled = false;


    public lookatLerper(Node3D looker, Vector3 target, float speed, float xAxisLimit, float yAxisLimit){
		
		helper = new Node3D();

		looker.AddChild(helper);
        //We're assuming if target is zero then what we're actually trying to do is reset. This is a bad way to do this hehe
        if(target == Vector3.Zero){ 
            helper.RotationDegrees = Vector3.Zero; 
        }else { 
            helper.LookAt(target); 
        }
           
       

        helper.RotationDegrees = new Vector3(
            Math.Clamp(helper.RotationDegrees.X, -xAxisLimit, xAxisLimit),
            Math.Clamp(helper.RotationDegrees.Y, -yAxisLimit, yAxisLimit), 
            helper.RotationDegrees.Z);

        start = looker.RotationDegrees;
        this.target = target;
        this.looker = looker;
        this.speed = speed;


	}

    public override void _Process(double delta){

        if (t >= 1 || looker.RotationDegrees == helper.RotationDegrees || deathScheduled) {
            helper.QueueFree();
            QueueFree();
        }
        //looker.RotationDegrees = looker.RotationDegrees.Lerp(helper.RotationDegrees, t);
        looker.RotationDegrees = start.Lerp(helper.RotationDegrees, t);
        t+=(float)delta * speed;

        


    }

    public void scheduleDeath(){
        deathScheduled = true;
    }

}