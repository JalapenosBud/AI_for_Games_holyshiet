using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemAssignController {

    #region DELEGATES AND EVENTS
    public delegate void SwapID(int ID, Slot slot);
    public static event SwapID Swapping_ID;

    public delegate void RemoveItemFromSlot();
    public static event RemoveItemFromSlot RemoveItem;
    /*
     * when this method is called, the event will fire
     * so if i call this method from DragMe.cs
     * i'll have to subscribe to it here
     */ 
    public static void MethodSwapID(int ID, Slot slot)
    {
        SwapID attachItem = Swapping_ID;
        if(attachItem != null)
        {
            Swapping_ID(ID,slot);
        }
    }

    public static void MethodRemoveItem()
    {
        RemoveItemFromSlot removeItem = RemoveItem;
        if (removeItem != null)
        {
            RemoveItem();
        }
    }
    #endregion

    public void AssignCorrentObject(object o)
    {

    }

    public void AttachToSlot(int ID)
    {
        //TODO
        //
    }

}
