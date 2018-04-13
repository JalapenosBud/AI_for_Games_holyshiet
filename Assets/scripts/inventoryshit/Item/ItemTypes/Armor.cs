using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item{

    public override bool IsStackable
    {
        get
        {
            return false;
        }
    }

    public Armor(string name, Sprite sprite) : base(name, sprite)
    {
        stackable = false;
    }
    public Armor()
    {
    }
   
}
