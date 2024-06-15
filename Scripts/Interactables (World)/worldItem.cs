using Godot;
using System;



public partial class worldItem: interactable{



    [Export] Ammo ammo;
    [Export] heldItem heldItem;
    [Export] keyItem keyItem;

    [Export] String description;

    //Used for viewing the scene
    [Export] PackedScene DisplayScene;

    itemModelViewer viewer;
    
    

    public override void interact(){
        
        viewer = itemModelViewerManager.newViewer(DisplayScene);
        GetTree().Root.AddChild(viewer);
        viewer.setRotationMode(itemModelViewer.rotateMode.mouse);

        
        Node n = textWindowManager.loadScene();
        GetTree().Root.AddChild(n);
        
        textWindowManager.loadText(description);
        textWindowManager.addButton(take,"Take");
        textWindowManager.addButton(close,"Exit");
        textWindowManager.setViewportTexture(viewer.getViewport());
    }

    //Todo
    public void take(){
        textWindowManager.close();
        itemModelViewerManager.removeViewer(viewer);


        //Likely much better ways to do this, but this is better than my previous way - maintains types
        if(heldItem!= null){playerInventory.add(heldItem);}
        if(ammo!=null){playerInventory.add(ammo);}
        if(keyItem!=null){playerInventory.add(keyItem);}
        


        this.QueueFree();
        
    }

    public void close(){
        textWindowManager.close();
        itemModelViewerManager.removeViewer(viewer);

    }


}