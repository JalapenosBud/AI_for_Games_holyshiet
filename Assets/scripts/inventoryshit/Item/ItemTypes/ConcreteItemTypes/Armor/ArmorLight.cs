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

    public void ShowArmor()
    {
        Debug.Log("this guy have " + armorType + " equipped");
    }
}
