using Godot;
using System;

public partial class itemMarkerLerper : Node{

	float increment = 0.2f;
	bool lerping = false;
    float i = 0;


	TextureRect rect;
	float targetOpacity;
	Node3D pos;
	[Signal] public delegate void LerpFinishedEventHandler();

	public itemMarkerLerper(TextureRect rect, float targetOpacity, Node3D pos){
		this.rect = rect;
		this.targetOpacity = targetOpacity;
		this.pos = pos;
		lerping = true;
	}

	public override void _Process(double delta){

		if(lerping){
			float l;

			i +=(float)delta*increment;

            l = 0 + (1 - 0) * i;

            l = Math.Clamp(l, 0, 1);

			float start = rect.Modulate.A;
            float target = targetOpacity;

            float current = start * (1 - l) + target * l;

            Color colour = rect.Modulate;

            Color opacity = new Color(colour.R, colour.G, colour.B, current);

            rect.Modulate = opacity;


			if(l >= 1){
				lerping = false;
				EmitSignal(SignalName.LerpFinished);
				this.QueueFree();
			}

		}


	}
}
