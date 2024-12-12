using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacementInteractor : MonoBehaviour
{
    public Transform placementTrans;
    [SerializeField] private BoxCollider Collider;
    private IPlaceAble currentItem;

    private void Start()
    {
        Collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
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
