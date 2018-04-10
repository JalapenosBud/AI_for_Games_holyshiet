using UnityEngine;
using UnityEngine.UI;

public class GOSlot : MonoBehaviour {

    public Slot slot;
    public Text stackingTxt;

    public void Start()
    {
        if(slot != null)
        {
            GetComponent<Image>().sprite = slot.GetItemSprite();
        }

    }
}
