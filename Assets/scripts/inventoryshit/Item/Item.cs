using System.Threading;
using UnityEngine;

[System.Serializable]
public abstract class Item : IPrintItemName{

    protected string name;
    protected Sprite sprite;
    private int _id;

    protected IArmor armor;
    protected IConsumable consumable;

    protected EnumArmor armorType;
    protected EnumConsumables consumableType;

    private int _slotRefID;
    
    //an object to lock, that only exists one place in memory
    private static object sync = new object();
    //a static variable we increment to generate a unique ID for the items
    private static int counter = 0;

    public int SlotRefID { get { return _slotRefID;} set {_slotRefID = value; } }

    public int ID
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public Item() { }

    public Item(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
        //set to -1, cause it doesnt belong anywhere
        
        this.SlotRefID = -1;

        lock (sync)
        {
            //increment ID uniquely
            ID = Interlocked.Increment(ref counter);
        }

    }

    public EnumArmor GetEnumArmorType()
    {
        return armor.RetrieveEnumArmorType();
    }

    public void SetArmorType(IArmor armor)
    {
        this.armor = armor;
    }
    public IArmor GetArmor()
    {
        return armor;
    }

    public void SetConsumableType(IConsumable consumable)
    {
        this.consumable = consumable;
    }
    

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public string GetName()
    {
        return name;
    }

    public void PrintTheItemNames()
    {
        if (SlotRefID == -1)
            return;

        Debug.Log("Item ID: " + ID + " , name: " + name + " of type: " + armor.ShowArmor() + " at slotRefID " + SlotRefID);
    }
}
