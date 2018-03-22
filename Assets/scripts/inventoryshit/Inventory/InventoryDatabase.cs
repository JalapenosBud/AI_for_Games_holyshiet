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

        databaseList.Add(greenArmor);
        databaseList.Add(orangeArmor);

        
    }

    public void PrintAllClassNames()
    {
        foreach (var dbitem in databaseList)
        {
            dbitem.PrintTheItemNames();
        }
    }

    public void PrintArmorTypes()
    {
        foreach (var dbitem in databaseList)
        {
            //dbitem.();
        }
    }

    private Sprite LoadSprite(string spriteName)
    {
        return Resources.Load<Sprite>(spriteName);
    }
    
    //TODO
    //method here to get item's id and its corresponding sprite
	
}
