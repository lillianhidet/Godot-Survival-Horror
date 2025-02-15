using Godot;
using System;


public partial class invImageLerper : Node{

    invSlot parent;
    invSlot targetParent;

    float increment = 2f;

    invSlotItem c;
    TextureRect t;

    bool lerping = false;
    float i = 0;

	[Signal] public delegate void LerpFinishedEventHandler();
    public invImageLerper(invSlotItem c, invSlot parent, invSlot targetParent){
        this.parent = parent;
        this.targetParent = targetParent;
        this.c = c;
        lerping = true;
        
    }

    public override void _Process(double delta)
    {
        if(lerping){
			float l;

			i +=(float)delta*increment;

            l = 0 + (1 - 0) * i;

            l = Math.Clamp(l, 0, 1);

            c.GlobalPosition = parent.GlobalPosition.Lerp(targetParent.GlobalPosition, l);
            //c.Scale = parent.Scale.Lerp(targetParent.Scale, l);
            c.Size = parent.Size.Lerp(targetParent.Size, l);


            //opacity 
            float start = parent.targetOpacity;
            float target = targetParent.targetOpacity;

            float current = start * (1 - l) + target * l;

            Color colour = c.Modulate;

            Color opacity = new Color(colour.R, colour.G, colour.B, current);

            c.Modulate = opacity;
            
            

            if(l >= 1){
                lerping = false;
                c.Reparent(targetParent);
                targetParent.inSlot = c;
                EmitSignal(SignalName.LerpFinished);
                Inventory_UI.itemsMoving = false;
				this.QueueFree();
            }
        }
    }


}