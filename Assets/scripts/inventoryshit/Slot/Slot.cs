using UnityEngine;

[System.Serializable]
public abstract class Slot {

    public InventoryType SlotType;
    public Sprite sprite;

    public int ID;

    //need to instantiate this somewhere, or get it from somewhere
    private Item tmpItem;
    public EnumArmor enumArmor;
    public Slot()
    {
        
    }

    public Slot(int id)
    {

    }

    public Item GetItem()
    {
        return tmpItem;
    }

    /*public static bool DoesItemTypeMatch(InventoryType type1, Item item, Slot slot)
    {
        if (type1 == InventoryType.CHAR_EQUIPMENT && item.GetEnumArmorType() == slot.enumArmor)
        {
            return true;
        }
        return false;
    }*/


    public static bool DoesSlotTypeMatchCharEquip(InventoryType type1)
    {
        if(type1 == InventoryType.CHAR_EQUIPMENT)
        {
            return true;
        }
        return false;
    }

    public static bool DoesSlotTypeMatchBagEquip(InventoryType type1)
    {
        if (type1 == InventoryType.BAG)
        {
            return true;
        }
        return false;
    }

    //if tmpItem has been assigned to some value in this class
    //return true
    public bool DoWeContainAnItem()
    {
        if( tmpItem != null)
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
    /*
     * ID at this slot should be equal to the new item passed in
     * and we get the new item id from the slot where the drag ends
     */ 

    public void RemoveItem(Item item)
    {
        item.SlotRefID = -1;
        item = null;
    }

    public void RemoveItem()
    {
        tmpItem.SlotRefID = -1;
        tmpItem = null;
    }

    public void UpdateItemIDAtSlot(Item item)
    {
        tmpItem = item;
        tmpItem.SlotRefID = ID;
        Debug.Log("SLOT ID is: " + ID + " and newItem SlotRefID is: " + item.SlotRefID);
    }

    public Sprite GetItemSprite()
    {
        //make this into temp item
        if (tmpItem == null)
            return null;
        return tmpItem.GetSprite();
    }
}
