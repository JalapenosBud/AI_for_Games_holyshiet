using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : Slot{

    private EnumArmor enumArmor;

	public CharacterSlot(EnumArmor enumArmor) : base()
    {
        this.enumArmor = enumArmor;
    }
}
