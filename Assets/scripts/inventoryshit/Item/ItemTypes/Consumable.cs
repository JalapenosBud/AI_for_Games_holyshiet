using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public override bool IsStackable
    {
        get
        {
            //sæt en begrænsning på stack, men det er måske mere op til hvor inventory skal bruges
            return stackable;
        }
        
    }

    public Consumable(string name, Sprite sprite) : base(name, sprite)
    {
        stackable = true;
    }
    public Consumable()
    {
    }
}
