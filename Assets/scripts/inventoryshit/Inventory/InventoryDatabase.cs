using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDatabase {

    public static List<Item> databaseList = new List<Item>();

    public InventoryDatabase()
    {
        databaseList.Add(new Item("green", Resources.Load<Sprite>("green")));
        databaseList.Add(new Item("orange", Resources.Load<Sprite>("orange")));

        foreach (var dbitem in databaseList)
        {
            dbitem.PrintClassName();
        }
    }

    
    //TODO
    //method here to get item's id and its corresponding sprite
	
}
