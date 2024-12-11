using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class PlacementInteractor : MonoBehaviour
{
    public Transform placementTrans;
    private IPlaceAble currentItem;
    private BoxCollider Collider;

    private void Start()
    {
        Collider = GetComponent<BoxCollider>();
        Collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IPlaceAble placeAbleItem))
        {
            currentItem = placeAbleItem;
            currentItem.PlacementParentTrans = placementTrans;
            currentItem.InArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentItem == null) return;
        currentItem.InArea = false;
        currentItem.PlacementParentTrans = null;
    }
}
