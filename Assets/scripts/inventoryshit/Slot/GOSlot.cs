using UnityEngine;
using UnityEngine.UI;

public class GOSlot : MonoBehaviour {

    public Slot slot;
    public Text stackingTxt;

    public void Start()
    {
        stackingTxt = GetComponentInChildren<Text>();
        if (slot != null)
        {
            GetComponent<Image>().sprite = slot.GetItemSprite();
            if(slot.DoWeContainAnItem())
            {
                if(slot.GetItem() is Consumable)
                {
                    BagSlot bagSlot = (BagSlot)slot;
                    
                    // stackingTxt.text = slot.GetItem().
                    //stackingTxt = GetComponentInChildren<Text>();
                    stackingTxt.text = bagSlot.CurrentStackCount.ToString();
                }
            }
        }

    }

    
}
