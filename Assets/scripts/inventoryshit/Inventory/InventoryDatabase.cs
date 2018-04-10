using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class InventoryDatabase {

    public List<Item> databaseList;

    public InventoryDatabase()
    {
        databaseList = new List<Item>();
        CreateArmor();
        UpdateItemTypes();

        CreateConsumables();
        //UpdateConsumableTypes();
        UpdateItemTypes();
        //UpdateAllArmorTypes();
        PrintAllClassNames();
        
    }

    private void CreateConsumables()
    {
        Consumable health_potion = new Consumable("health_pot", LoadSprite("purple"));
        health_potion.SetConsumableType(new Potion(EnumConsumables.Potion));

        //Consumable apple = new Consumable("apple", LoadSprite("green"));
        //apple.SetConsumableType(new )

        databaseList.Add(health_potion);
    }

    private void CreateArmor()
    {
        //maybe have some constructor that auto puts the item into the list
        Armor greenArmor = new Armor("green", LoadSprite("green"));
        greenArmor.SetArmorType(new ArmorLight(EnumArmor.Chest));

        Armor orangeArmor = new Armor("orange", LoadSprite("orange"));
        orangeArmor.SetArmorType(new ArmorLight(EnumArmor.Head));

        Armor purpleArmor = new Armor("purple", LoadSprite("purple"));
        purpleArmor.SetArmorType(new ArmorLight(EnumArmor.Boots));

        Armor blueArmor = new Armor("blue", LoadSprite("blue"));
        blueArmor.SetArmorType(new ArmorLight(EnumArmor.Gloves));

        Armor yellowArmor = new Armor("yellow", LoadSprite("yellow"));
        yellowArmor.SetArmorType(new ArmorLight(EnumArmor.Legs));

        Armor redArmor = new Armor("red", LoadSprite("red"));
        redArmor.SetArmorType(new ArmorLight(EnumArmor.Neck));

        Armor brownArmor = new Armor("brown", LoadSprite("brown"));
        brownArmor.SetArmorType(new ArmorLight(EnumArmor.Weapon));

        Armor anotherBrownie = new Armor("brownie", LoadSprite("brown"));
        anotherBrownie.SetArmorType(new ArmorLight(EnumArmor.Boots));

        Armor redBeard = new Armor("redBeard", LoadSprite("red"));
        redBeard.SetArmorType(new ArmorLight(EnumArmor.Head));

        databaseList.Add(greenArmor);
        databaseList.Add(orangeArmor);
        databaseList.Add(purpleArmor);
        databaseList.Add(blueArmor);
        databaseList.Add(yellowArmor);
        databaseList.Add(redArmor);
        databaseList.Add(brownArmor);
        databaseList.Add(anotherBrownie);
        databaseList.Add(redBeard);
    }

    public void UpdateItemTypes()
    {
        foreach(Item item in databaseList)
        {
            if(item is Armor)
            {
                Armor tmpArmor = (Armor)item;
                tmpArmor.GetArmor().AssignArmorType();
            }
            if(item is Consumable)
            {
                item.GetConsumable().RetrieveEnumConsumableType();
            }
            
            //IArmor armor = armors as IArmor;
            //if (armor != null)
            //{
            //    armor.AssignArmorType();
            //    Debug.Log(armor);
            //}
            
        }
        //Debug.Log("Item types are updated");
    }

    public void PrintAllClassNames()
    {
        databaseList.ForEach(x => x.PrintTheItemNames());
    }

    private Sprite LoadSprite(string spriteName)
    {
        return Resources.Load<Sprite>(spriteName);
    }
    
    //TODO
    //method here to get item's id and its corresponding sprite
	
}
