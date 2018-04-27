using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagSlot : Slot{
    public List<Item> stackedItems = new List<Item>();
    //lav currentstackamount om, eller sæt den til stackedItems.count
    //
    public int currentStackCount;
    //have this to know what slot it should ref to
    public Slot slot;

    public Text text;

    public BagSlot() : base()
    {
        SlotType = InventoryType.BAG;
    }
    /// <summary>
    /// Amount to split stack with
    /// </summary>
    /// <param name="amount"></param>
    public void SplitStackedItems(int amount)
    {
        if (amount > currentStackCount || amount == 0 || currentStackCount <= 1)
        {
            return;
        }
        else
        {
            stackedItems.RemoveRange(0, amount);
        }

    }

    /*
     * add to stackeditems as many times as amount tells it to
     */
    public void AddToStackedItems(int amount, Item item)
    {
        for (int i = 0; i < amount; i++)
        {
            stackedItems.Add(item);
        }
    }
    
	
}
