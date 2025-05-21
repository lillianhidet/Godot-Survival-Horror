using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Inventory_UI : Control{

	CanvasLayer canvas;

	[Export] RichTextLabel itemName;
	[Export] RichTextLabel itemDesc;
	[Export] Button wepButton;
	[Export] Button consumButton;
	[Export] Button keyButton;
	public bool isOpen {get; set;}
	[Export] PackedScene inv_item;

	List<InventoryItem> itemsDisplaying;

	[Export] invSlot[] slots;
	public static bool itemsMoving = false;

	invSlotItem selected = null;
	int middle;

	public override void _Ready(){
		canvas = GetNode<CanvasLayer>("canvas");
		canvas.Visible = false;

		wepButton.Pressed += displayWeapons;
		consumButton.Pressed += displayConsumables;
		keyButton.Pressed += displayKeyItems;
		
	}

	public void open(List<InventoryItem> itemsToDisplay){
		itemsDisplaying = itemsToDisplay;

		isOpen = true;
		canvas.Visible = true;
		middle = (int)(slots.Length/2 + 0.5f);
		int nextSlot = middle;

		int totalFilled = 0;
		bool lastNeg = true;

		int currentItem = 0;

		if(itemsToDisplay.Count > 0){
			
			if(slots.Length >= MathF.Abs(nextSlot) + 1){

				for(int l = 0; l < slots.Length; l++ ){

				
					InventoryItem item = itemsToDisplay[currentItem];
					
					if(slots[nextSlot].inSlot == null){

						invSlotItem itemHolder = (invSlotItem) inv_item.Instantiate();
						invSlot slot = slots[nextSlot];
						
						addItemtoSlot(slot, itemHolder, item);

						if(lastNeg){
							totalFilled++;
							nextSlot = middle+totalFilled;
							lastNeg = false;
						}else{
							nextSlot = middle-totalFilled;
							lastNeg = true;
						}

					}
					
					if(currentItem + 1 > itemsToDisplay.Count-1 ){
						currentItem = 0;
					}else{
						currentItem++;
					}

				}
			}
			updateSelected(middle);
			

		}else{/*GD.Print("No items to display");*/}

	}

		
	public void reset(){
		selected = null;
		foreach(invSlot c in slots){
			c.inSlot = null;
			foreach(Node n in c.GetChildren()){
				n.QueueFree();
			}
		}
	}


	public void exit(){
		isOpen = false;
		canvas.Visible = false;
		reset();
	}

	public void moveSelectionRight(){
		//TODO:
		//get the leftmost slot
		//get the item BELOW the one that used to be in that slot
		//if that doesn't exist, get the first item in the list

		if(!itemsMoving){

			invImageLerper lerper;
			itemsMoving = true;

			invSlot first = slots[0];
			invSlotItem firstSlotItem = first.inSlot;


				for(int i = 0; i < slots.Length; i++){

					invSlot parent = slots[i];

					if(parent.inSlot != null){

						invSlotItem slot = parent.inSlot;

						if(i + 1 > slots.Length-1){
							parent.inSlot = null;
							slot.QueueFree();
							
						}else{

							invSlot target = slots[i+1];
							lerper = new invImageLerper(slot, parent, target);
							slot.AddChild(lerper);


						}
					}else{GD.PushWarning("Empty inventory slot (Should not be possible)");}
				}

				InventoryItem invItem = firstSlotItem.item;
				int index = itemsDisplaying.IndexOf(invItem);
				invSlotItem slotItemNew = (invSlotItem) inv_item.Instantiate();

				if(index-1 < 0){
					addItemtoSlot(first, slotItemNew, itemsDisplaying[0]);
				}else{
					addItemtoSlot(first, slotItemNew, itemsDisplaying[index-1]);
				}

				
			
			updateSelected(middle-1);
		}

		
	}


	public void moveSelectionLeft(){


		if(!itemsMoving){

			invImageLerper lerper;
			itemsMoving = true;

			invSlot last = slots[slots.Length-1];
			invSlotItem lastSlotItem = last.inSlot;

			for(int i = 0; i < slots.Length; i++){

				invSlot parent = slots[i];

				if(parent.inSlot != null){

					invSlotItem slot = parent.inSlot;

					if(i - 1 < 0){
						
						//slot.QueueFree();
						//parent.inSlot = null;
					}else{

						invSlot target = slots[i-1];
						lerper = new invImageLerper(slot, parent, target);
						slot.AddChild(lerper);


					}
				}else{GD.PushWarning("Empty inventory slot (Should not be possible!!!)");}
			}

			InventoryItem invItem = lastSlotItem.item;
			int index = itemsDisplaying.IndexOf(invItem);
			invSlotItem slotItemNew = (invSlotItem) inv_item.Instantiate();

			if(index+1 > itemsDisplaying.Count-1){
					addItemtoSlot(last, slotItemNew, itemsDisplaying[0]);
				}else{
					addItemtoSlot(last, slotItemNew, itemsDisplaying[index+1]);
				}
			updateSelected(middle+1);
		}
		
	}

	public void addItemtoSlot(invSlot slot, invSlotItem slotItem, InventoryItem inventoryItem){
		slot.inSlot = slotItem;
		slot.AddChild(slotItem);

		slotItem.item = inventoryItem;

		slotItem.Size = slot.Size;

		Color col = new Color(slotItem.Modulate.R, slotItem.Modulate.G, slotItem.Modulate.B, slot.targetOpacity);
		slotItem.Modulate = col;


		//Create a modelviewer
		itemModelViewer viewer = itemModelViewerManager.newViewer(inventoryItem.DisplayScene);
		viewer.setItemSize(0.5f);
		slotItem.AddChild(viewer);
		//GetTree().Root.AddChild(viewer);
		viewer.setRotationMode(itemModelViewer.rotateMode.none);
		viewer.resetRotation();
		slotItem.Texture = viewer.getViewport().GetTexture();

	}

	public void updateSelected(int fixedMiddle){
		if(selected == null){
			selected = slots[fixedMiddle].inSlot;
		}

		itemModelViewer viewer = selected.GetChild<itemModelViewer>(0);
		viewer.setRotationMode(itemModelViewer.rotateMode.none);
		viewer.resetRotation();

		selected = slots[fixedMiddle].inSlot;
		viewer = selected.GetChild<itemModelViewer>(0);
		viewer.setRotationMode(itemModelViewer.rotateMode.auto);

		itemName.Text = selected.item.name;
		itemDesc.Text = selected.item.description;

		
	}

	public void displayWeapons(){
		reset();
		open(playerInventory.weapons.ToList<InventoryItem>());

	}
	public void displayConsumables(){
		reset();
		open(playerInventory.consumables);

	}

	public void displayKeyItems(){
		reset();
		open(playerInventory.keyItems.ToList<InventoryItem>());

	}

	
}
