using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemTypeAssigner  {

	void AssignItemArmor(Armor armor);
    void AssignItemConsumable(Consumables consumables);
}
