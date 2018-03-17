using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemAssignController {

    #region DELEGATES AND EVENTS
    public delegate void AttachItemToSlot(Item item);
    public static event AttachItemToSlot AttachItem;

    public delegate void RemoveItemFromSlot();
    public static event RemoveItemFromSlot RemoveItem;

    public static void MethodAttachItem(Item item)
    {
        AttachItemToSlot attachItem = AttachItem;
        if(attachItem != null)
        {
            AttachItem(item);
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
