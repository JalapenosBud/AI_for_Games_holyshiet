using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item : IPrintOutStringNewClass{

    private string name;
    private Sprite sprite;
    private int _id;

    private int _slotRefID;

    //an object to lock, that only exists one place in memory
    private static object sync = new object();
    //a static variable we increment to generate a unique ID for the items
    private static int counter;

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

    public void PrintClassName()
    {
        Debug.Log("ID: " + ID + " , name: " + name + " at slotRefID " + SlotRefID);
    }
}
