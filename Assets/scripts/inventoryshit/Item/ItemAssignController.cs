using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemAssignController {

    #region DELEGATES AND EVENTS
    public delegate void SwapID(int ID);
    public static event SwapID Swapping_ID;

    public delegate void RemoveItemFromSlot();
    public static event RemoveItemFromSlot RemoveItem;

    public static void MethodSwapID(int ID)
    {
        SwapID attachItem = Swapping_ID;
        if(attachItem != null)
        {
            Swapping_ID(ID);
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
