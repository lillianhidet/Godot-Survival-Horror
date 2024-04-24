using Godot;
using System;

public partial class Ammo : InventoryItem{

    enum ammoType{
        pistol,
        rifle
    }

    [Export] ammoType aType;

    [Export] int amount;

    public override void _Ready(){
        type = ItemType.Ammo;
    }


}