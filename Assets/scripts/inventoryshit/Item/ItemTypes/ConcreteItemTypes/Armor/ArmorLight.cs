using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorLight : IArmor
{
    private EnumArmor armorType;

    public ArmorLight(EnumArmor armorType)
    {
        this.armorType = armorType;
    }

    public EnumArmor AssignArmorType()
    {
        return this.armorType;
    }

    public string ShowArmor()
    {
        return "this guy have " + armorType + " equipped";
    }
}
