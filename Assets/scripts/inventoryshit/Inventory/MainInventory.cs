using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainInventory  : MonoBehaviour{

    public GameObject bagSlotsPanel;
    public GameObject charSlotsPanel;
    private SlotIncrementer slotIncrementer;

    private GOSlot[] bagSlots;

    private GOSlot[] characterSlots;

    private List<GOSlot> allSlots = new List<GOSlot>();

    private Item tmpItem;
    private Item tmpItemOtherSlot;

    InventoryDatabase inventoryDatabase;

    public int i;
    public void Start()
    {
        //SUB TO EVENTS
        ItemAssignController.Getting_First_ID += ItemAssignController_Getting_First_ID;
        ItemAssignController.Getting_Second_ID += ItemAssignController_Getting_Second_ID;

        //INSTANTIATE
        inventoryDatabase = new InventoryDatabase();
        slotIncrementer = new SlotIncrementer();

        CreateCharacterEquipSlots();
        CreateBagSlots();

        //TODO: fixme; for now: dummy code 
        // slots[0].slot.AssignSlotRefID(InventoryDatabase.databaseList[0]);
        // slots[1].slot.AssignSlotRefID(InventoryDatabase.databaseList[1]);
        AddItemToSlot("orange");
        AddItemToSlot("green");
        AddItemToSlot("purple");
        AddItemToSlot("blue");
        AddItemToSlot("red");
        AddItemToSlot("red");
        AddItemToSlot("red");
        AddItemToSlot("green");

        inventoryDatabase.PrintAllClassNames();
    }

    private void AddItemToSlot(string name)
    {
        while( i <= bagSlots.Length)
        {
            if(!bagSlots[i].slot.DoWeContainAnItem())
            {
                bagSlots[i].slot.AssignSlotRefID(LookUpItem(name));
                print("name: " + bagSlots[i].slot.GetItem().GetName() + " SlotRefID " + bagSlots[i].slot.GetItem().SlotRefID);
                i++;
                
                break;
            }
            //GetItem().SlotRefID == -1
        }
    }

    private Item LookUpItem(string name)
    {
        Item holdItem = null;
        foreach (var item in InventoryDatabase.databaseList)
        {
            if(name.Equals(item.GetName()))
            {
                holdItem = item;
            }
        }
        return holdItem;
    }
    

    //when event fires, this method will get called
    private void ItemAssignController_Getting_First_ID(Item item)
    {
        /*
         * when at destination slot, get that slot id
         * cache the initial slot info, and then swap when arriving
         */
        tmpItem = item;
    }
    /*
     * 
     * the item we have in this class, gets assigned to the parameter
     * and the slot that gets used in the parameter, calls the method to update item id
     * 
     */ 
    private void ItemAssignController_Getting_Second_ID(Item item, Slot slot)
    {
        //save that object the cursor lands on
        tmpItemOtherSlot = item;
        //now set the object the cursor landed on, to the old "began dragged" object to tmpItem
        item = tmpItem;

        //access GOSLOT array at the old index
        bagSlots[tmpItem.SlotRefID].slot.UpdateItemIDAtSlot(tmpItemOtherSlot);

        //update item at the new slot
        slot.UpdateItemIDAtSlot(item);

        //assign swapped images properly
        bagSlots[tmpItem.SlotRefID].GetComponent<Image>().sprite = tmpItem.GetSprite();
        bagSlots[tmpItemOtherSlot.SlotRefID].GetComponent<Image>().sprite = tmpItemOtherSlot.GetSprite();


    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Break();
        }
    }

    //CALL THIS SECOND #2
    //refactor: fit to all kind of containers
    private void CreateBagSlots()
    {
        //make a root "node"
        //assign it values
        
        bagSlots = bagSlotsPanel.GetComponentsInChildren<GOSlot>();
        bagSlots[0].slot = new BagSlot();
        bagSlots[0].slot.ID = 0;
        Debug.Log("root ID " + bagSlots[0].slot.ID);
        //set the id to be incremented
        slotIncrementer.counter = bagSlots[0].slot.ID;
        for (int i = 1; i < bagSlots.Length; i++)
        {
            //make new slot
            bagSlots[i].slot = new BagSlot();
            //increment the counter for each new instantiation
            slotIncrementer.Increment(slotIncrementer.counter);
            //now set the ID to the newly incremented counter
            //but only for this object
            bagSlots[i].slot.ID = slotIncrementer.counter;
            Debug.Log("slot: " + bagSlots[i].slot.ID);
            //Debug.Log("CI: " + createIncrement.counter);
        }
    }
    /*
     * character slot start at 0
     * bagslot starts at where characterslot ended
     * keep id in the same array
     * 
     */ 
     //CALL THIS FIRST #1
    private void CreateCharacterEquipSlots()
    {
        if (charSlotsPanel == null)
        {
            return;
        }
        characterSlots = bagSlotsPanel.GetComponentsInChildren<GOSlot>();
        characterSlots[0].slot = new BagSlot();
        characterSlots[0].slot.ID = 0;
        Debug.Log("root ID " + characterSlots[0].slot.ID);
        //set the id to be incremented
        slotIncrementer.counter = characterSlots[0].slot.ID;
        for (int i = 1; i < bagSlots.Length; i++)
        {
            //make new slot
            foreach (EnumArmor eee in Enum.GetValues(typeof (EnumArmor)))
            {
                characterSlots[i].slot = new CharacterSlot(eee);
                break;
            }
            
            //increment the counter for each new instantiation
            slotIncrementer.Increment(slotIncrementer.counter);
            //now set the ID to the newly incremented counter
            //but only for this object
            characterSlots[i].slot.ID = slotIncrementer.counter;
            Debug.Log("slot: " + characterSlots[i].slot.ID);
            //Debug.Log("CI: " + createIncrement.counter);
        }


        //must be called in the end to sum id up
        allSlots.AddRange(characterSlots);
    }


}
