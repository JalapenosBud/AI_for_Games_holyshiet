using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item, IArmor {
    
    public Armor(string name, Sprite sprite, EnumArmor enumArmor) : base(name, sprite)
    {
        armorType = enumArmor;
    }
    public Armor() { }

    public string ShowArmor()
    {
        throw new System.NotImplementedException();
    }

    public EnumArmor AssignArmorType()
    {
        return armorType;
    }
}
