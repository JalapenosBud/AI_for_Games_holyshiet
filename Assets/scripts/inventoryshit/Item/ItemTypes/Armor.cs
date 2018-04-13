using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item{

    protected override bool IsStackable
    {
        get
        {
            return false;
        }
    }

    public Armor(string name, Sprite sprite) : base(name, sprite)
    {

    }
    public Armor()
    {
    }
   
}
