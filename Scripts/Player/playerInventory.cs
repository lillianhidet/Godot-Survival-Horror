using Godot;
using System;
using System.Collections.Generic;

public partial class playerInventory : Node{

	public static List<InventoryItem> weapons;
	public static List<InventoryItem> consumables;
	public static List<keyItem> keyItems;
	public static List<Ammo> ammo;


	public static void addItem(InventoryItem i){

		if(i.type == InventoryItem.ItemType.KeyItem){
			
			
		}else if(i.type == InventoryItem.ItemType.Consumable){
			

		}else if(i.type == InventoryItem.ItemType.Ammo){


		}

		
	}


}
