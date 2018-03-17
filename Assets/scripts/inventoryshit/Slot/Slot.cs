using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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
