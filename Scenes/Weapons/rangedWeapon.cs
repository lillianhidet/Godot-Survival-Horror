using Godot;
using System;

public partial class rangedWeapon : heldItem{

	[Export] Ammo.ammoType ammoType;
	[Export] muzzleFlash flash;
	[Export] int ammoCapacity;
	int ammoLoaded = 0;
	[Export] float fireRate;

	//For testing
	[Export] RayCast3D raycast;

	[Export] Node3D aimingAt;

	bool readyToFire = true;
	public new void use(){
		if(readyToFire && ammoLoaded != 0){
			readyToFire = false;

			GetTree().CreateTimer(fireRate).Timeout += () => readyToFire = true;
			flash.flash(0.15f);

			ammoLoaded--;
			hudManager.updateLoadedAmmoLabel(ammoLoaded);
			
			if(raycast.IsColliding()){
				
				if(raycast.IsInGroup("hitBox")){
					hitBox hit = (hitBox)raycast.GetScript();
				
				}
			}
		}
	}

	public void reload(int maxAmount){
		ammoLoaded = maxAmount;
	}

	public Ammo.ammoType ammoUsed(){return ammoType;}

	public int getLoaded(){return ammoLoaded;}
	public int getCapacity(){return ammoCapacity;}

    public override void _Process(double delta){
		//This logic should be handled elsewhere
		if(playerState.IsAiming){
        	Vector2 screen = GetViewport().GetCamera3D().UnprojectPosition(aimingAt.GlobalTransform.Origin);
			hudManager.setreticulePos(screen);
		}else{
			hudManager.hideReticule();
		}
    }

    //GetTree().CreateTimer(timeToOpenEyes).Timeout += () => openEyes();

}
