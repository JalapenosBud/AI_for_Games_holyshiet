using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOSlot : MonoBehaviour {

    public Slot slot;

    private Image img;

    public void Start()
    {
        //ItemAssignController.AttachItem += ItemAssignController_AttachItem;
        //ItemAssignController.RemoveItem += ItemAssignController_RemoveItem;
    }

    private void ItemAssignController_RemoveItem()
    {
        //slot.SetTmpItem(null);
    }

    /*
     * når man har attached et item til et slot
     * så 1. skift image på slot
     * 2. opdater slot.id reference til nyt item
     * 
     */ 
    private void ItemAssignController_AttachItem(Item item)
    {
        //item.SetID(slot.GetID());
    }
}
