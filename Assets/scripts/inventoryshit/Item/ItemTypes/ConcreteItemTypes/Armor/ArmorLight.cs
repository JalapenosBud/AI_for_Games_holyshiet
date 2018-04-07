using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorLight : Armor, IArmor
{

    public ArmorLight(EnumArmor armorType)
    {
        this.armorType = armorType;
    }

    public EnumArmor AssignArmorType()
    {
        return armorType;
    }

    public EnumArmor RetrieveEnumArmorType()
    {
        return armorType;
    }

    public string ShowArmor()
    {
        throw new System.NotImplementedException();
    }
}
