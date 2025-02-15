using Godot;
using System;

public partial class InventoryItem : Node{

	public enum ItemType {
    	KeyItem, 
    	Ammo, 
    	Consumable,
		Held
    }

	[Export] public ItemType type;

	[Export] public string name;
	[Export] public string description;

	//[Export] public Texture2D image;

	[Export] public PackedScene DisplayScene;



	public virtual void use(){}

	
}
