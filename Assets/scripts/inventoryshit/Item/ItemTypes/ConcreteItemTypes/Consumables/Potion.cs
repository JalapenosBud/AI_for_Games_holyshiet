using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Consumable, IConsumable
{
    public Potion(EnumConsumables consumableType)
    {
        this.consumableType = consumableType;
    }

    public EnumConsumables AssignConsumableType()
    {
        return consumableType;
    }

    public EnumConsumables RetrieveEnumConsumableType()
    {
        return consumableType;
    }
}
