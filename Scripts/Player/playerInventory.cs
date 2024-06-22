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

	public static void add(Ammo a){
		//If an ammo stack has room for the new ammo, add it to that stack
		//Otherwise make a new stack

		//Don't love this, but are we ever going to be carrying stacks that it'll matter (Still think there has to be better ways)
		foreach(Ammo am in ammo){
			if(am.getType() == a.getType()){
				if(am.getAvailableSpace() >= a.getAmount()){
					am.increaseAmount(a.getAmount());

					//Bad way to do this
					a.reduceAmount(a.getAmount());
					break;
				}

			}
		}

		if(a.getAmount()>0){
			ammo.Add(a);
		}
		
	}

	public static void add(keyItem k){}


//Really this class should be entirely bypassed and the interaction class should just talk directly to the weapon for these two functions (maybe)
	public static void attack(){
		if(currentlyHeld != null){
				if(currentlyHeld.itemType == heldItem.type.ranged){
					((rangedWeapon)currentlyHeld).use();
				}
			}
	}

	//This really needs a second pass, did this while extremely tired
	public static void reload(){
		//get the relevant ammo type, use it to reload, return unused to inventory

		//Check if we have enough ammo to reload fully (up to the weapons capacity)
		//If we do, subtract the amount we need, keeping leftovers
		//If we don't, take as much as we can
		if(currentlyHeld.itemType == heldItem.type.ranged){

					rangedWeapon wep = (rangedWeapon)currentlyHeld;

					Ammo.ammoType typeUsed = wep.ammoUsed();
					List<Ammo> toRemove = new List<Ammo>();

					int toLoad = 0;

					foreach(Ammo a in ammo){

						if(a.getType() == typeUsed){
							//If the amount in the ammobox is less than or exactly enough to refill the weapon
							if(a.getAmount() + wep.getLoaded() + toLoad <= wep.getCapacity()){
								toLoad+=a.getAmount();
								toRemove.Add(a);

								//if(ammo.Count == 0){break;}
								//If a box has more than enough to reload 
							}else{
								//Get the difference, take it from the box
								//Need to factor in toLoad
								GD.Print("reduced");
								a.reduceAmount(wep.getCapacity() - (wep.getLoaded() + toLoad));
								toLoad = wep.getCapacity() - wep.getLoaded();
								break;
								
							}

						}
					}

					foreach(Ammo a in toRemove){
						ammo.Remove(a);
					}

					wep.reload(toLoad);
			}
		}
}
