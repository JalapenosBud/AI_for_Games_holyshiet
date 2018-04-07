using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmor {

    string ShowArmor();

    EnumArmor AssignArmorType();

    EnumArmor RetrieveEnumArmorType();
}
