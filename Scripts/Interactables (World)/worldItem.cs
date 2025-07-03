using Godot;
using System;



public partial class worldItem: interactable{



    //[Export] Ammo ammo;
    //[Export] heldItem heldItem;
    //[Export] keyItem keyItem;

    [Export] InventoryItem item;
    [Export(PropertyHint.MultilineText)] String pickupText;
    [Export] float displaySize = 1;

    //Global colours reference?
    [Export] Color yesCol;
    [Export] Color noCol;

    //[Export] String description;

    //Used for viewing the scene
    //[Export] PackedScene DisplayScene;

    itemModelViewer viewer;



    public override void interact(){
       
        viewer = itemModelViewerManager.newViewer(item.DisplayScene);
        GetTree().Root.AddChild(viewer);
        viewer.setRotationMode(itemModelViewer.rotateMode.mouse);


       

        viewer.lerpSize(0, displaySize);

        //formatDescription();
        Node n = textWindowManager.loadObjScene(viewer.getViewport(), pickupText);
        GetTree().Root.AddChild(n);
     
        textWindowManager.addButton(take, "Yes", yesCol);
        textWindowManager.addButton(close, "No", noCol);

        Visible = false;
    }

    //Todo refacto this, this should just store a generic Inventory Item - (Which can then identify its type)
    public void take(){
        textWindowManager.close();
        itemModelViewerManager.removeViewer(viewer);

        if(item.type == InventoryItem.ItemType.KeyItem){
            playerInventory.add((keyItem)item);
        }else if(item.type == InventoryItem.ItemType.Ammo){
            playerInventory.add((Ammo)item);
        }else if(item.type == InventoryItem.ItemType.Consumable){
            playerInventory.add((consumable)item);
        }else if(item.type == InventoryItem.ItemType.Held){
            playerInventory.add((heldItem)item);
        }

        //Likely much better ways to do this, but this is better than my previous way - maintains types
        /*if(heldItem!= null){
            playerInventory.add(heldItem);
        }

        //Temporary Fix - need to duplicate the item
        if(ammo!=null){
            Ammo a = new Ammo();
            a.capacity = ammo.capacity;
            a.amount = ammo.amount;
            a.type = ammo.type;
            playerInventory.add(a);
            
        }

        if(keyItem!=null){
            playerInventory.add(keyItem);
            }*/
        //new Ammo(ammo.getType(), ammo.getCapacity(), ammo.getAmount())
        this.QueueFree();
           
    }
    public void formatDescription(){
        if(item.type == InventoryItem.ItemType.Ammo){
            Ammo ammo = (Ammo) item;
            item.description = item.description.Replace("{a}", ammo.amount.ToString());
            item.description = item.description.Replace("{t}", ammo.aType.ToString().Capitalize());
        }
    }

    public void close(){
        textWindowManager.close();
        itemModelViewerManager.removeViewer(viewer);
        Visible = true;

    }


}