using Godot;
using System;



public partial class worldItem: interactable{



    [Export] InventoryItem item;
    [Export] PackedScene ObjScene;

    ObjViewer viewer;
    
    

    public override void interact(){
        
        viewer = itemModelViewerManager.newViewer(ObjScene);
        GetTree().Root.AddChild(viewer);

        
        Node n = textWindowManager.loadScene();
        GetTree().Root.AddChild(n);
        
        textWindowManager.loadText("Take?");
        textWindowManager.setViewportTexture(viewer.getViewport());



    }
    public override void _Ready(){

    }

    //Todo
    public void take(){
        playerInventory.addItem(item);
        //add item to inventory
    }

    public void close(){
        itemModelViewerManager.removeViewer(viewer);

    }

}