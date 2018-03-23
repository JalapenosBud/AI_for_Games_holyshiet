using System.Collections.Generic;
using UnityEngine;

public class InventoryDatabase {

    public static List<Item> databaseList;

    public InventoryDatabase()
    {
        databaseList = new List<Item>();

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

        Armor brownArmor = new Armor("purple", LoadSprite("purple"));
        brownArmor.SetArmorType(new ArmorLight(EnumArmor.Weapon));

        databaseList.Add(greenArmor);
        databaseList.Add(orangeArmor);
        databaseList.Add(purpleArmor);
        databaseList.Add(blueArmor);
        databaseList.Add(yellowArmor);
        databaseList.Add(redArmor);
        databaseList.Add(brownArmor);
        
    }

    public void PrintAllClassNames()
    {
        foreach (var dbitem in databaseList)
        {
            dbitem.PrintTheItemNames();
        }
    }

    private Sprite LoadSprite(string spriteName)
    {
        return Resources.Load<Sprite>(spriteName);
    }
    
    //TODO
    //method here to get item's id and its corresponding sprite
	
}
