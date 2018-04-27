using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMe : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public bool dragOnSurface = true;
    private GameObject draggedObj;
    private RectTransform draggingPlane;
    private Canvas canvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas = eventData.pointerPress.GetComponent<Canvas>();
        var goSlotVar = eventData.pointerEnter;
        //if(goSlotVar.GetComponent<GOSlot>().slot.GetItem() is Consumable)

        draggedObj = new GameObject("img");
        draggedObj.transform.SetParent(canvas.transform.GetChild(0), false);

        //put it in the bottom of the hieriarchy so it appears infront of other elements
        draggedObj.transform.SetAsLastSibling();
        draggedObj.layer = 8;

        //add an image to the dragged obj
        var image = draggedObj.AddComponent<Image>();
        

        //TODO
        //refactor this with code from Slot.cs 
        if (goSlotVar != null)
        {
            image.sprite = goSlotVar.GetComponent<GOSlot>().slot.GetItemSprite();
            
            print("img color " + image.sprite + " item name: " + goSlotVar.gameObject.GetComponent<GOSlot>().slot.GetItem());
            
        }
        //if we're dragging on surface
        if (dragOnSurface)
        {
            //set the draggingplane equal to a RectTransform transform (which is a GUI transform)
            draggingPlane = transform as RectTransform;
        }
        else
        {
            //else set it as the canvas transform
            draggingPlane = canvas.transform as RectTransform;
        }

        ItemAssignController.GettingFirstIDMethod(goSlotVar.GetComponent<GOSlot>().slot.GetItem());
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedObj != null)
        {
            PutDragAtMouse(eventData);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedObj != null)
        {
            Destroy(draggedObj);
        }
        var goSlotVar = eventData.pointerEnter;

        //pre: NO ITEM AT SLOT
        //post: place item
        if(goSlotVar.GetComponent<GOSlot>().slot.GetItem() == null)
        {
            //TODO: copy paste this into the else statement below
            if(Slot.DoesSlotTypeMatchCharEquip(goSlotVar.GetComponent<GOSlot>().slot.SlotType))
            {
                ItemAssignController.JustCheckForArmorEnum(goSlotVar.GetComponent<GOSlot>().slot);
            }
            
            if(Slot.DoesSlotTypeMatchBagEquip(goSlotVar.GetComponent<GOSlot>().slot.SlotType))
            {
                ItemAssignController.JustPlaceItemAtIDMethod(goSlotVar.GetComponent<GOSlot>().slot);
            }
        }
        //pre: ITEM AT SLOT
        //post: item has been swapped
        else
        {
            
            ItemAssignController.GettingIDForSwapMethod(goSlotVar.GetComponent<GOSlot>().slot.GetItem(), goSlotVar.GetComponent<GOSlot>().slot);
                   
        }
    }

    void PutDragAtMouse(PointerEventData data)
    {
        var pos = (data.position - (Vector2)canvas.GetComponent<RectTransform>().localPosition);
        draggedObj.GetComponent<RectTransform>().localPosition = pos;
    }

   /* private void SetDraggedPosition(PointerEventData data)
    {
        //if dragging on surface (yes) and the mouse entered an objects fov
        //and it belongs to a recttransform
        if (dragOnSurface && data.pointerEnter != null
            && data.pointerEnter.transform as RectTransform != null)
        {
            //set the plane we're dragging on to where the mouse entered the recttransform
            draggingPlane = data.pointerEnter.transform as RectTransform;
        }

        var rt = draggedObj.GetComponent<RectTransform>();
        Vector3 glmousepos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, data.position, data.pressEventCamera, out glmousepos))
        {
            rt.position = glmousepos;
            rt.rotation = draggingPlane.rotation;
        }
    }*/

    //this apparently fires when letting go of mouse button (0)
    //maybe this is the magic for onenddrag???
    public void OnPointerClick(PointerEventData eventData)
    {
        var goSlotVar = eventData.pointerEnter;

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (goSlotVar.gameObject.GetComponent<GOSlot>().slot.GetItem() is Armor)
            {

                //this gets item we click on and assign it to tmpItem in MainInventory.cs
                ItemAssignController.GettingFirstIDMethod(goSlotVar.GetComponent<GOSlot>().slot.GetItem());
                //then this decides what item should be placed or swapped
                ItemAssignController.RightClickToEquipMethod(goSlotVar.GetComponent<GOSlot>().slot);
            }
            else if(goSlotVar.gameObject.GetComponent<GOSlot>().slot.GetItem() is Consumable)
            {
                //TODO: implement logic for character stats to boost whatever stat when right clicking
                print("consumed 1 unit");
            }
        }
        
    }
}
