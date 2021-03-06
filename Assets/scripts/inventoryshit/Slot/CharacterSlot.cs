﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : Slot{

    //public EnumArmor enumArmor;

	public CharacterSlot(EnumArmor enumArmor) : base()
    {
        this.enumArmor = enumArmor;
        SlotType = InventoryType.CHAR_EQUIPMENT;
    }

    public CharacterSlot() : base()
    {
        SlotType = InventoryType.CHAR_EQUIPMENT;
    }
}
