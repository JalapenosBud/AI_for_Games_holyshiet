using UnityEngine;
using UnityEngine.UI;

public class MainInventory  : MonoBehaviour{

    public GameObject slotspanel;
    private SlotIncrementer slotIncrementer;

    private GOSlot[] slots;

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

        CreateBagSlots();

        //TODO: fixme; for now: dummy code 
        // slots[0].slot.AssignSlotRefID(InventoryDatabase.databaseList[0]);
        // slots[1].slot.AssignSlotRefID(InventoryDatabase.databaseList[1]);
        AddItemToSlot("orange");
        AddItemToSlot("green");
        AddItemToSlot("purple");
        AddItemToSlot("blue");
        AddItemToSlot("red");

        inventoryDatabase.PrintAllClassNames();
    }

    private void AddItemToSlot(string name)
    {
        while( i < slots.Length)
        {
            if(!slots[i].slot.DoWeContainAnItem())
            {
                slots[i].slot.AssignSlotRefID(LookUpItem(name));
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
        slots[tmpItem.SlotRefID].slot.UpdateItemIDAtSlot(tmpItemOtherSlot);

        //update item at the new slot
        slot.UpdateItemIDAtSlot(item);

        //assign swapped images properly
        slots[tmpItem.SlotRefID].GetComponent<Image>().sprite = tmpItem.GetSprite();
        slots[tmpItemOtherSlot.SlotRefID].GetComponent<Image>().sprite = tmpItemOtherSlot.GetSprite();


    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Break();
        }
    }


    //refactor: fit to all kind of containers
    private void CreateBagSlots()
    {
        //make a root "node"
        //assign it values
        slots = slotspanel.GetComponentsInChildren<GOSlot>();
        slots[0].slot = new BagSlot();
        slots[0].slot.ID = 0;

        //set the id to be incremented
        slotIncrementer.counter = slots[0].slot.ID;
        for (int i = 1; i < slots.Length; i++)
        {
            //make new slot
            slots[i].slot = new BagSlot();
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
