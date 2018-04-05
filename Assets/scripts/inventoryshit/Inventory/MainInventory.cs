using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainInventory  : MonoBehaviour{

    public GameObject bagSlotsPanel;
    public GameObject charSlotsPanel;
    private SlotIncrementer slotIncrementer;

    private List<GOSlot> bagSlots;

    private List<GOSlot> characterSlots;

    public List<GOSlot> allSlots = new List<GOSlot>();

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
        //TODO
        CreateBagSlots();

        //TODO: fixme; for now: dummy code 
        // slots[0].slot.AssignSlotRefID(InventoryDatabase.databaseList[0]);
        // slots[1].slot.AssignSlotRefID(InventoryDatabase.databaseList[1]);
        AddItemToBagSlot("orange");
        AddItemToBagSlot("green");
        AddItemToBagSlot("purple");
        AddItemToBagSlot("blue");
        AddItemToBagSlot("red");
        AddItemToBagSlot("red");
        AddItemToBagSlot("red");
        AddItemToBagSlot("green");
        AddItemToBagSlot("yellow");
        //add char slots
        //AddItemToCharEquipment("red");

        bagSlots.ForEach(x => print("armor name: " + x.slot.GetItem().GetName() + " at SlotRefID " + x.slot.GetItem().SlotRefID));

        //inventoryDatabase.PrintAllClassNames();
    }

    private void AddItemToBagSlot(string name)
    {
        while( i <= bagSlots.Count)
        {
            if(!bagSlots[i].slot.DoWeContainAnItem())
            {
                bagSlots[i].slot.AssignSlotRefID(LookUpItem(name));
                //print("name: " + bagSlots[i].slot.GetItem().GetName() + " SlotRefID " + bagSlots[i].slot.GetItem().SlotRefID);
                i++;
                
                break;
            }
            //GetItem().SlotRefID == -1
        }
    }

    /*have this check for what ArmorEnum that belongs to each item
     * so when an item gets added, loop through the list, and when enums matches
     * then assign that slotRefID to that SlotID, so Head goes to slot 0 etc
     */ 
    private void AddItemToCharEquipment(string name)
    {
        for (int i = 0; i < characterSlots.Count; i++)
        {
            if(!characterSlots[i].slot.DoWeContainAnItem())
            {
                if(InventoryDatabase.databaseList.Contains(LookUpItem(name)))
                {
                    //TODO
                    /*not a string but an enum
                     * 
                     */ 
                }
            }
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
        allSlots[tmpItem.SlotRefID].slot.UpdateItemIDAtSlot(tmpItemOtherSlot);

        //update item at the new slot
        slot.UpdateItemIDAtSlot(item);

        //assign swapped images properly
        allSlots[tmpItem.SlotRefID].GetComponent<Image>().sprite = tmpItem.GetSprite();
        allSlots[tmpItemOtherSlot.SlotRefID].GetComponent<Image>().sprite = tmpItemOtherSlot.GetSprite();


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
        var newCounter = allSlots[characterSlots.Count - 1].slot.ID;
        newCounter++;
        bagSlots = bagSlotsPanel.GetComponentsInChildren<GOSlot>().ToList();
        bagSlots[0].slot = new BagSlot();
        bagSlots[0].slot.ID = 0;
        //Debug.Log("root ID " + bagSlots[0].slot.ID);
        //set the id to be incremented
        slotIncrementer.counter = bagSlots[0].slot.ID;
        for (int i = 1; i < bagSlots.Count; i++)
        {
            //make new slot
            bagSlots[i].slot = new BagSlot();
            //increment the counter for each new instantiation
            slotIncrementer.Increment(slotIncrementer.counter);
            //now set the ID to the newly incremented counter
            //but only for this object
            bagSlots[i].slot.ID = slotIncrementer.counter;
            //Debug.Log("slot: " + bagSlots[i].slot.ID + " has " + bagSlots[i].slot.GetItem());
            //Debug.Log("CI: " + createIncrement.counter);
        }
        allSlots.AddRange(bagSlots);
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
        var armorEnum = EnumArmor.Head;
        characterSlots = charSlotsPanel.GetComponentsInChildren<GOSlot>().ToList();
        characterSlots[0].slot = new CharacterSlot(EnumArmor.Head);
        characterSlots[0].slot.ID = 0;
        //Debug.Log("root ID " + characterSlots[0].slot.ID);
        //set the id to be incremented
        slotIncrementer.counter = characterSlots[0].slot.ID;
        for (int i = 1; i < characterSlots.Count; i++)
        {
            armorEnum++;
            characterSlots[i].slot = new CharacterSlot();
            //make new slot
            foreach (EnumArmor eee in Enum.GetValues(typeof(EnumArmor)))
            {
                armorEnum = eee;
                if (!characterSlots[i - 1].slot.enumArmor.Equals(armorEnum) && armorEnum > characterSlots[i-1].slot.enumArmor)
                {
                    characterSlots[i].slot.enumArmor = armorEnum;
                    break;
                }
            }

            //increment the counter for each new instantiation
            slotIncrementer.Increment(slotIncrementer.counter);
            //now set the ID to the newly incremented counter
            //but only for this object
            characterSlots[i].slot.ID = slotIncrementer.counter;
            //Debug.Log("slot: " + characterSlots[i].slot.ID + " with " + characterSlots[i].slot.enumArmor);
            //Debug.Log("CI: " + createIncrement.counter);
        }


        //must be called in the end to sum id up
        allSlots.AddRange(characterSlots);
        
        /*TODO:
         * when checking for slotrefid, check here
         * so bagslot[0]s slotrefid has to be larger than characterslots[characterslots.length] --  > -- 
         * 
         * 
         */ 
    }


}
