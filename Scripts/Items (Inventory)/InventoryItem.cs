using Godot;
using System;

public partial class InventoryItem : Node{

	public enum ItemType {
    	KeyItem, 
    	Ammo, 
    	Consumable,
		Held
    }

	public ItemType type;

	[Export] Texture2D image;



	public virtual void use(){}

	
}
