using System.Collections.Generic;
using UnityEngine;

public class InventoryDatabase {

    public static List<Item> databaseList = new List<Item>();

    public InventoryDatabase()
    {


        //databaseList.Add(new Item("green", Resources.Load<Sprite>("green")));
        //databaseList.Add(new Item("orange", Resources.Load<Sprite>("orange")));

        
    }

    public void PrintAllClassNames()
    {
        foreach (var dbitem in databaseList)
        {
            dbitem.PrintTheItemNames();
        }
    }

    
    //TODO
    //method here to get item's id and its corresponding sprite
	
}
