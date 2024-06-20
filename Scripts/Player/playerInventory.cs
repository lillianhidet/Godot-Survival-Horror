using Godot;
using System;
using System.Collections.Generic;

//Tracks the items the player has, and dispatches equip requests to the equipManager
public partial class playerInventory : Node{

	public static List<heldItem> weapons;
	public static List<InventoryItem> consumables;
	public static List<keyItem> keyItems;
	public static List<Ammo> ammo;

	public static bool holdingLantern;

	public static heldItem currentlyHeld;

	public static bool holdingOneHanded = false;
	public static bool holdingTwoHanded = false;

	public static heldItem leftHandSlot;
	public static heldItem rightHandSlot;

    public override void _Ready()
    {
        weapons = new List<heldItem>();
		consumables = new List<InventoryItem>();
		keyItems = new List<keyItem>();
		ammo = new List<Ammo>();
		holdingLantern = false;
    }
    public static void add(heldItem h){
		//This will only be called once, is this how this should be done?
		if(h.itemType == heldItem.type.lantern){
			EquipManager.equipLantern(h);
			holdingLantern = true;

		}else{

			weapons.Add(h);
			if(currentlyHeld == null){
				if(h.itemStyle == heldItem.equipStyle.twoHanded && !holdingLantern){
					EquipManager.equipitemBoth(h);
					currentlyHeld = h;
				}else if(h.itemStyle == heldItem.equipStyle.oneHanded){
					EquipManager.equipItemRight(h);
					currentlyHeld = h;

				}
			}

		}
		
		
		//Spawn the item in the players hand (Should have a seperate class for managing physical equipping/unequipping)
	}

	public static void add(Ammo a){}

	public static void add(keyItem k){}

	public static void attack(){
		if(currentlyHeld != null){
				if(currentlyHeld.itemType == heldItem.type.ranged){
					((rangedWeapon)currentlyHeld).use();
				}
			}
	}

	public static void reload(){
		//get the relevant ammo type, use it to reload, return unused to inventory

		//Temp, for testing only
		if(currentlyHeld.itemType == heldItem.type.ranged){
					((rangedWeapon)currentlyHeld).reload(8);
				}
		}
}
