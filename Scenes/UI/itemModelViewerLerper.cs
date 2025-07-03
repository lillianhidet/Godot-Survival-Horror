using Godot;
using System;

public partial class itemModelViewerLerper : Node
{
	float increment = 0.3f;
	bool lerping = false;
    float i = 0;


	float targetSize;
	float startSize;
	Node3D node;
	//[Signal] public delegate void LerpFinishedEventHandler();

	public itemModelViewerLerper(Node3D node, float startSize, float targetSize)
	{
		this.node = node;
		this.targetSize = targetSize;
		this.startSize = startSize;
		lerping = true;
	}

	public override void _Process(double delta){

		if(lerping){
			increment = increment + 0.02f;

			i +=(float)delta*increment;


            i = Math.Clamp(i, 0, 1);

			if (!IsInstanceValid(node))
			{
				QueueFree();
			} else {

                float current = startSize * (1 - i) + targetSize * i;

                node.Scale = new Vector3(current, current, current);

                //hmm
                if (i >= 1)
                {
                    lerping = false;
                    //EmitSignal(SignalName.LerpFinished);
                    this.QueueFree();
                }

            }

			

		}


	}
}
