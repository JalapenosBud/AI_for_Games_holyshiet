using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemTypeAssigner  {

	void AssignItemArmor(EnumArmor armor);
    void AssignItemConsumable(EnumConsumables consumables);
}
