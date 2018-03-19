﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot : IPrintOutStringNewClass {

    public InventoryType SlotType;
    public Sprite sprite;

    public int ID;

    //need to instantiate this somewhere, or get it from somewhere
    private Item tmpItem;

    public Slot()
    {
       
    }

    public Slot(int id)
    {

    }

    public Item GetItem()
    {
        return tmpItem;
    }

    //if tmpItem has been assigned to some value in this class
    //return true
    public bool DoWeContainAnItem()
    {
        if(tmpItem != null)
        {
            return true;
        }
        return false;
    }

    //here pass slot.ID into the argument
    //so if slot 1 gets called, the item is gonna have a ref to slot 1
    public void AssignSlotRefID(Item item)
    {
        if(DoWeContainAnItem())
        {
            return;
        }
        else
        {
            tmpItem = item;
            tmpItem.SlotRefID = ID;
        }
        
    }
    /*
     * ID at this slot should be equal to the new item passed in
     * and we get the new item id from the slot where the drag ends
     */ 

    public void UpdateItemIDAtSlot(Item item)
    {
        tmpItem = item;
        tmpItem.SlotRefID = ID;
        Debug.Log("SLOT ID is: " + ID + " and newItem ID is: " + item.SlotRefID);
    }

    public Sprite GetItemSprite()
    {
        //make this into temp item
        return tmpItem.GetSprite();
    }


    public void PrintClassName()
    {
        Debug.Log(ToString() + " " + ID);
    }
}