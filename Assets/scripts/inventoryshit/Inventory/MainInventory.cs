using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainInventory  : MonoBehaviour{

    public GameObject slotspanel;
    private SlotIncrementer slotIncrementer;

    private GOSlot[] slots;

    InventoryDatabase inventoryDatabase;
    public void Start()
    {
        ItemAssignController.Swapping_ID += ItemAssignController_Swapping_ID;
        inventoryDatabase = new InventoryDatabase();
        slotIncrementer = new SlotIncrementer();
        ManipulateSlots();
        //TODO: fixme; for now: dummy code 
        slots[0].slot.AssignSlotRefID(InventoryDatabase.databaseList[0]);
        slots[1].slot.AssignSlotRefID(InventoryDatabase.databaseList[1]);
    }

    //when event fires, this method will get called
    private void ItemAssignController_Swapping_ID(int ID, Slot slot)
    {
        slot.UpdateItemIDAtSlot(slot.GetItem());
        print("called from maininventory");
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Break();
        }
    }


    //refactor: fit to all kind of containers
    private void ManipulateSlots()
    {
        //make a root "node"
        //assign it values
        slots = slotspanel.GetComponentsInChildren<GOSlot>();
        slots[0].slot = new Slot();
        slots[0].slot.ID = 0;

        //set the id to be incremented
        slotIncrementer.counter = slots[0].slot.ID;
        for (int i = 1; i < slots.Length; i++)
        {
            //make new slot
            slots[i].slot = new Slot();
            //increment the counter for each new instantiation
            slotIncrementer.Increment(slotIncrementer.counter);
            //now set the ID to the newly incremented counter
            //but only for this object
            slots[i].slot.ID = slotIncrementer.counter;
            Debug.Log("slot: " + slots[i].slot.ID);
            //Debug.Log("CI: " + createIncrement.counter);
        }
    }


}
