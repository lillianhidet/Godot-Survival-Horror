using Godot;
using System;

public partial class Ammo : InventoryItem{

    public enum ammoType{
        pistol,
        rifle
    }

    [Export] ammoType aType;

    [Export] int capacity;
    [Export] int amount;

    public override void _Ready(){
        type = ItemType.Ammo;
    }

    public ammoType getType(){return aType;}
    public int getAmount(){return amount;}
    public int getAvailableSpace(){return capacity - amount;}
    public void reduceAmount(int amt){amount -= amt;}
    public int increaseAmount(int add){
        if((add + amount) <= capacity){
            amount += add;
            return 0;
        }
        else{

            int spillover = (amount + add) - capacity;
            amount = capacity;
            return spillover;
        }

    }

}