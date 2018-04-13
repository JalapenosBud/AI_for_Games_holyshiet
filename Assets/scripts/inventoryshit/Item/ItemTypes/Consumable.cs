using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    protected override bool IsStackable
    {
        get
        {
            //sæt en begrænsning på stack, men det er måske mere op til hvor inventory skal bruges
            return stackAmount > 0;
        }
        
    }

    public Consumable(string name, Sprite sprite) : base(name, sprite)
    {
    }
    public Consumable()
    {
    }
}
