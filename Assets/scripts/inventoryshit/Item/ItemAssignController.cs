public class ItemAssignController {

    #region DELEGATES AND EVENTS
    public delegate void GetFirstID(Item item);
    public static event GetFirstID Getting_First_ID;

    public delegate void GetIDForSwap(Item item, Slot slot);
    public static event GetIDForSwap Getting_ID_FOR_SWAP;

    public delegate void PlaceItemAtID(Slot slot);
    public static event PlaceItemAtID JustPlaceItemAtID;

    #endregion
    /*
     * when this method is called, the event will fire
     * so if i call this method from DragMe.cs
     * i'll have to subscribe to it here
     */
    public static void GettingFirstIDMethod(Item item)
    {
        GetFirstID firstID = Getting_First_ID;
        if(firstID != null)
        {
            Getting_First_ID(item);
        }
    }

    public static void GettingIDForSwapMethod(Item item, Slot slot)
    {
        GetIDForSwap secondID = Getting_ID_FOR_SWAP;
        if (secondID != null)
        {
            Getting_ID_FOR_SWAP(item, slot);
        }
    }

    public static void JustPlaceItemAtIDMethod(Slot slot)
    {
        PlaceItemAtID placeItemAtID = JustPlaceItemAtID;
        if(placeItemAtID != null)
        {
            JustPlaceItemAtID(slot);
        }
    }

}
