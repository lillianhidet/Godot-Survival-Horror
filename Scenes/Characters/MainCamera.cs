using Godot;
using System;

public partial class MainCamera : Camera3D
{
	public static MainCamera instance {get; private set;}

    public override void _Ready()
    {
        instance = this;
    }

    public Boolean isVisibleOnScreen(Area3D area, uint layer){

		var from = ProjectRayOrigin(GetViewport().GetMousePosition());
		var end = from + ProjectRayNormal(UnprojectPosition(area.GlobalTransform.Origin)) * 5000;
		var query = PhysicsRayQueryParameters3D.Create(from, end);
		query.CollideWithAreas = true;
		query.CollisionMask = layer;

		
		var result = GetWorld3D().DirectSpaceState.IntersectRay(query);
		

		if(result.Keys.Count > 0){
			var raycasthit = result["rid"];
			if(area.GetRid() == (Rid) raycasthit){
				return true;
			}else{
				return false;
			}

		}else{
			return false;
		}
		

		

	}


}
