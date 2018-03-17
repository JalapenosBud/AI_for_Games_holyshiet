﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class DragMe : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public bool dragOnSurface = true;

    private CanvasGroup canvasGroup;
    private GameObject draggedObj;
    private RectTransform draggingPlane;


    private void Start()
    {
        //print("calling from dragme");
    }

    public void DetectCanvas(PointerEventData data)
    {
        
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //make a canvas variable where we get the canvas component in the mouseclick
        var canvas = eventData.pointerPress.GetComponent<Canvas>();
        //set the object to be dragged to a new GO
        print(canvas);
        //TODO:
        //this variable is null when clicking
        
        draggedObj = new GameObject("img");

        //set the dragged objs parent to the canvas transform
        draggedObj.transform.SetParent(canvas.transform, false);

        //put it in the bottom of the hieriarchy so it appears infront of other elements
        draggedObj.transform.SetAsLastSibling();
        
        //add an image to the dragged obj
        var image = draggedObj.AddComponent<Image>();
        var goSlotVar = eventData.pointerEnter.GetComponent<GOSlot>();



        //TODO
        //refactor this with code from Slot.cs 
        if (goSlotVar != null)
        {
            image.sprite = goSlotVar.slot.GetItemSprite();
            print(image.sprite);
        }
        //image.sprite = 

        //image.sprite 
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

        //method to set the data equal to where we started the drag from
        SetDraggedPosition(eventData);

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedObj != null)
        {
            SetDraggedPosition(eventData);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedObj != null)
        {
            //attaches new item
            //ItemAssignController.MethodAttachItem();
            //removes old
            //ItemAssignController.MethodRemoveItem();
            Destroy(draggedObj);
        }
    }

    private void SetDraggedPosition(PointerEventData data)
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var mesg = eventData.pointerEnter;
        if (mesg.GetComponent<GOSlot>() != null)
        {
            Debug.Log(mesg.GetComponent<GOSlot>().slot.ID);
        }
    }
}
