using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotIncrementer {

    public int counter;

    public void Increment(int id)
    {
        id++;
        counter = id;

    }
}
