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

    private Item oldItem;
    private Item tmpItem;
    private Item tmpItemOtherSlot;

    InventoryDatabase inventoryDatabase;

    public int i;
    public void Start()
    {
        //SUB TO EVENTS
        ItemAssignController.Getting_First_ID += ItemAssignController_Getting_First_ID;
        ItemAssignController.Getting_ID_FOR_SWAP += ItemAssignController_Getting_ID_FOR_ITEM_SWAP;
        ItemAssignController.JustPlaceItemAtID += ItemAssignController_JustPlaceItemAtID;
        ItemAssignController.CheckForArmorEnum += ItemAssignController_CheckForArmorEnum;
        ItemAssignController.RightClickToEquip += ItemAssignController_RightClickToEquip;
        //INSTANTIATE
        inventoryDatabase = new InventoryDatabase();
        slotIncrementer = new SlotIncrementer();

        CreateCharacterEquipSlots();
        //TODO
        CreateBagSlots();

        //TODO: fixme; for now: dummy code 
        // slots[0].slot.AssignSlotRefID(InventoryDatabase.databaseList[0]);
        // slots[1].slot.AssignSlotRefID(InventoryDatabase.databaseList[1]);
        AddStackableItemToBagSlot("health_pot", 5);
        //ARMOR ITEMS
        AddItemToBagSlot("orange");
        AddItemToBagSlot("green");
        //AddItemToBagSlot("apple");
        AddItemToBagSlot("blue");
        AddItemToBagSlot("red");
        AddItemToBagSlot("yellow");
        AddItemToBagSlot("brown");
        AddItemToBagSlot("redBeard");
        AddItemToBagSlot("brownie");
        //CONSUMABLE ITEMS
        //AddItemToBagSlot("health_pot");
        
        //add char slots
        //AddItemToCharEquipment("redBeard");
        //inventoryDatabase.UpdateAllArmorTypes();
       // inventoryDatabase.UpdateConsumableTypes();
        //bagSlots.ForEach(x => print("armor name: " + x.slot.GetItem().GetArmor().RetrieveEnumArmorType() + " at SlotRefID " + x.slot.GetItem().SlotRefID + " at a: " + x.slot.SlotType));

        //inventoryDatabase.PrintAllClassNames();
    }

    /// <summary>
    /// This decides to place item if destination slot is empty, or swap if its not.
    /// </summary>
    /// <param name="slot">The slot the mouse pointer is at</param>
    private void ItemAssignController_RightClickToEquip(Slot slot)
    {
        if(slot.SlotType == InventoryType.BAG)
        {
            foreach (var charSlots in characterSlots)
            {
                //if slot's item we right click on == char equip slot armor
                if(slot.GetItem().GetEnumArmorType() == charSlots.slot.enumArmor)
                {
                    if(!charSlots.slot.DoWeContainAnItem())
                    {
                        //place item from the slot we click on
                        //into the char equip slot
                        PlaceItem(slot.GetItem(), charSlots.slot.ID);
                        break;
                    }
                    else
                    {
                        //param1 get the item in the slot we hover over
                        //param2 insert into the matching slot on char equip
                        SwapItem(charSlots.slot.GetItem(), charSlots.slot);
                        break;
                    }
                    
                }
            }
        }
        else if(slot.SlotType == InventoryType.CHAR_EQUIPMENT)
        {
            PlaceItemFromCharSlotToBagSlot(slot.GetItem());
        }
    }

    //pre: enum is the same
    //post: item has been placed
    private void ItemAssignController_CheckForArmorEnum(Slot slot)
    {
        if(!slot.DoWeContainAnItem())
        {
            if (oldItem.GetEnumArmorType() == slot.enumArmor)
            {
                PlaceItem(slot);
                print("placed item " + oldItem.GetEnumArmorType() + " at " + slot.enumArmor);
            }
        }
        
    }

    /// <summary>
    /// This places the item if the slot has a null item
    /// </summary>
    /// <param name="slot">The slot the mouse pointer is at</param>
    private void ItemAssignController_JustPlaceItemAtID(Slot slot)
    {
        PlaceItem(slot);
    }

    //when event fires, this method will get called
    /// <summary>
    /// This caches the item mouse pointer is at, into tmpItem and oldItem
    /// </summary>
    /// <param name="item">The item the mouse pointer is at</param>
    private void ItemAssignController_Getting_First_ID(Item item)
    {
        /*
         * when at destination slot, get that slot id
         * cache the initial slot info, and then swap when arriving
         */
        tmpItem = item;
        oldItem = item;
    }
    /*
     * 
     * the item we have in this class, gets assigned to the parameter
     * and the slot that gets used in the parameter, calls the method to update item id
     * 
     */
    /// <summary>
    /// This decides how to swap item depending on its a BAG or a CHAR slot
    /// </summary>
    /// <param name="item">The item the mouse pointer is at</param>
    /// <param name="slot">The slot the mouse pointer is at</param>
    private void ItemAssignController_Getting_ID_FOR_ITEM_SWAP(Item item, Slot slot)
    {
        if(slot.SlotType == InventoryType.BAG)
        {
            SwapItem(item, slot);
        }
        else if(slot.SlotType == InventoryType.CHAR_EQUIPMENT)
        {
            SwapAtCharEquipSlot(item, slot);
        }
        
    }

    /// <summary>
    /// Swapping when dragging from bag to char slot
    /// </summary>
    /// <param name="item">The item the mouse pointer is at</param>
    /// <param name="slot">The slot the mouse pointer is at</param>
    private void SwapAtCharEquipSlot(Item item, Slot slot)
    {
        //save that object the cursor lands on
        tmpItemOtherSlot = item;
        
        if(tmpItem.GetEnumArmorType() != item.GetEnumArmorType() 
            && tmpItem.GetEnumArmorType() != slot.GetItem().GetEnumArmorType())
        {
            return;
           
        }
        else
        {
            //now set the object the cursor landed on, to the old "began dragged" object to tmpItem
            item = tmpItem;

            //access GOSLOT array at the old index
            allSlots[tmpItem.SlotRefID].slot.UpdateItemIDAtSlot(tmpItemOtherSlot);

            //update item at the new slot
            slot.UpdateItemIDAtSlot(item);

            //assign swapped images properly
            allSlots[tmpItem.SlotRefID].GetComponent<Image>().sprite = tmpItem.GetSprite();
            allSlots[tmpItemOtherSlot.SlotRefID].GetComponent<Image>().sprite = tmpItemOtherSlot.GetSprite();
            print("placed item " + item.GetEnumArmorType() + " at " + slot.GetItem().GetEnumArmorType());
        }
    }

    /// <summary>
    /// This method swap items from BAG slot to CHAR slots
    /// </summary>
    /// <param name="item">The item the mouse pointer is at.</param>
    /// <param name="slot">The slot the mouse pointer is at.</param>
    private void SwapItem(Item item, Slot slot)
    {
        //if landing on a bag item, then check if the old item was in char equip
        //if it was, and the new armorType doesnt match old, return

        //save that object the cursor lands on
        tmpItemOtherSlot = item;

        if (allSlots[tmpItem.SlotRefID].slot.SlotType == InventoryType.CHAR_EQUIPMENT
            && tmpItem.GetEnumArmorType() != item.GetEnumArmorType())
            return;

        //now set the object the cursor landed on, to the old "began dragged" object to tmpItem
        item = tmpItem;

        //access GOSLOT array at the old index
        allSlots[tmpItem.SlotRefID].slot.UpdateItemIDAtSlot(tmpItemOtherSlot);

        //update item at the new slot
        slot.UpdateItemIDAtSlot(item);

        //assign swapped images properly
        allSlots[tmpItem.SlotRefID].GetComponent<Image>().sprite = tmpItem.GetSprite();
        allSlots[tmpItemOtherSlot.SlotRefID].GetComponent<Image>().sprite = tmpItemOtherSlot.GetSprite();

        print("placed item " + item.GetEnumArmorType() + " at " + slot.GetItem().GetEnumArmorType());
    }
    
    /// <summary>
    /// Places item at the slot the mouse cursor lands on.
    /// </summary>
    /// <param name="slot"></param>
    private void PlaceItem(Slot slot)
    {
        allSlots[oldItem.SlotRefID].GetComponent<Image>().sprite = null;
        allSlots[oldItem.SlotRefID].slot.RemoveItem();
        allSlots[slot.ID].slot.AssignSlotRefID(tmpItem);
        allSlots[slot.ID].GetComponent<Image>().sprite = tmpItem.GetSprite();
    }

    /// <summary>
    /// Puts item that gets clicked on from bag slot, to char slot.
    /// Takes firstly clicked item from allSlots array and puts it into charslots(id) element
    /// </summary>
    /// <param name="item"></param>
    /// <param name="id"></param>
    private void PlaceItem(Item item, int id)
    {
        allSlots[oldItem.SlotRefID].GetComponent<Image>().sprite = null;
        allSlots[oldItem.SlotRefID].slot.RemoveItem();
        characterSlots[id].slot.AssignSlotRefID(tmpItem);
        characterSlots[id].GetComponent<Image>().sprite = tmpItem.GetSprite();
    }

    //maybe reverse it so it starts from slot 0
    private void PlaceItemFromCharSlotToBagSlot(Item item)
    {
        for (int i = bagSlots.Count - 1; i >= 0; i--)
        {
            if(bagSlots[i].slot.DoWeContainAnItem())
            {
                continue;
            }
            else
            {
                allSlots[item.SlotRefID].GetComponent<Image>().sprite = null;
                allSlots[item.SlotRefID].slot.RemoveItem();

                bagSlots[i].slot.AssignSlotRefID(item);
                bagSlots[i].GetComponent<Image>().sprite = tmpItem.GetSprite();
                //print("item returned at: " + bagSlots[i].slot.ID);
            }
        }
    }


    /*item only exists once, so dont try to add several to bags, with the same name, if multiple of same items are needed
     * create several of same type
     * 
     */
    /// <summary>
    /// Adds an item to the bag slots after looking up the name.
    /// </summary>
    /// <param name="name">Matches item name in the inventory.</param>
    private void AddItemToBagSlot(string name)
    {
        while( i <= bagSlots.Count)
        {
            if(!bagSlots[i].slot.DoWeContainAnItem())
            {
                bagSlots[i].slot.AssignSlotRefID(LookUpItemInDB(name));
                //print("name: " + bagSlots[i].slot.GetItem().GetName() + " SlotRefID " + bagSlots[i].slot.GetItem().SlotRefID);
                i++;
                break;
            }
        }
    }

    /*
     * first find out if theres an empty spot to put the stack at
     * then assign a bagslot
     * then cast that bagslot to a stackslot
     * 
     * 
     */

    private void AddStackableItemToBagSlot(string name, int amount)
    {
        
        //cast
        StackSlot stackSlot = new StackSlot();
        bagSlots[i].slot = stackSlot as BagSlot;

        stackSlot.AddToStackedItems(amount, LookUpItemInDB(name));
        AddItemToBagSlot(name);
    }


    /*have this check for what ArmorEnum that belongs to each item
     * so when an item gets added, loop through the list, and when enums matches
     * then assign that slotRefID to that SlotID, so Head goes to slot 0 etc
     */ 
    private void AddItemToCharEquipment(string name)
    {
        characterSlots[0].slot.AssignSlotRefID(inventoryDatabase.databaseList[8]);


        //for (int i = 0; i < characterSlots.Count; i++)
        //{
        //    if(!characterSlots[i].slot.DoWeContainAnItem())
        //    {
        //        if(inventoryDatabase.databaseList.Contains(LookUpItem(name)))
        //        {
        //        }
        //    }
        //}


    }

    private Item LookUpItemInDB(string name)
    {
        Item holdItem = null;
        foreach (var item in inventoryDatabase.databaseList)
        {
            if(name.Equals(item.GetName()))
            {
                holdItem = item;
            }
        }
        return holdItem;
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
        bagSlots[0].slot.ID = characterSlots.Count;
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
            Debug.Log("slot: " + characterSlots[i].slot.ID + " has " + characterSlots[i].slot.enumArmor + " and is a: " + characterSlots[i].slot.SlotType);
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
