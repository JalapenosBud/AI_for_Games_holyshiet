public class ItemAssignController {

    #region DELEGATES AND EVENTS
    public delegate void GetFirstID(Item item);
    public static event GetFirstID Getting_First_ID;

    public delegate void GetSecondID(Item item, Slot slot);
    public static event GetSecondID Getting_Second_ID;

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

    public static void GettingSecondIDMethod(Item item, Slot slot)
    {
        GetSecondID secondID = Getting_Second_ID;
        if (secondID != null)
        {
            Getting_Second_ID(item, slot);
        }
    }

}
