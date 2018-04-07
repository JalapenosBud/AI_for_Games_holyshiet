public class ItemAssignController {

    #region DELEGATES AND EVENTS
    public delegate void GetFirstID(Item item);
    public static event GetFirstID Getting_First_ID;

    public delegate void GetIDForSwap(Item item, Slot slot);
    public static event GetIDForSwap Getting_ID_FOR_SWAP;

    public delegate void PlaceItemAtID(Slot slot);
    public static event PlaceItemAtID JustPlaceItemAtID;
    public static event PlaceItemAtID CheckForArmorEnum;

    #endregion
    
    //this gets the id that gets dragged, and makes a temp object out of it
    public static void GettingFirstIDMethod(Item item)
    {
        GetFirstID firstID = Getting_First_ID;
        if(firstID != null)
        {
            Getting_First_ID(item);
        }
    }

    //takes new and old item and swaps it with new and old slot
    public static void GettingIDForSwapMethod(Item item, Slot slot)
    {
        GetIDForSwap secondID = Getting_ID_FOR_SWAP;
        if (secondID != null)
        {
            Getting_ID_FOR_SWAP(item, slot);
        }
    }

    //returns the slot that gets hovered over
    public static void JustPlaceItemAtIDMethod(Slot slot)
    {
        PlaceItemAtID placeItemAtID = JustPlaceItemAtID;
        if(placeItemAtID != null)
        {
            JustPlaceItemAtID(slot);
        }
    }

    public static void JustCheckForArmorEnum(Slot slot)
    {
        PlaceItemAtID checkArmor = CheckForArmorEnum;
        if(checkArmor != null)
        {
            CheckForArmorEnum(slot);
        }
    }

}
