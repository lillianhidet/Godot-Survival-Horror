using Godot;
using System;

public partial class MainCamera : Camera3D
{
	public static MainCamera instance {get; private set;}

    public override void _Ready()
    {
        instance = this;
    }

	[Export] RayCast3D raycast;

    public bool isVisibleOnScreen(Area3D area, uint layer){
		
		GodotObject hit = null;

		raycast.TargetPosition = raycast.ToLocal(area.GlobalPosition);
		raycast.ForceRaycastUpdate();

		if( raycast.IsColliding()){
			hit = raycast.GetCollider();
		}

		/*var from = ProjectRayOrigin(new Vector2(0,0));
		var end = from + ProjectRayNormal(UnprojectPosition(area.GlobalTransform.Origin)) * 5000;
		var query = PhysicsRayQueryParameters3D.Create(from, end);
		query.CollideWithAreas = true;
		query.CollideWithBodies = false;
		query.CollisionMask = layer;*/

		
		//var result = GetWorld3D().DirectSpaceState.IntersectRay(query);
		

		//if(result.Keys.Count > 0){
			//var raycasthit = result["rid"];
			//if(area.GetRid() == (Rid) raycasthit){
			if(area == hit){
				return true;
			}else{
				return false;
			}
			

	
		

		

	}


}
